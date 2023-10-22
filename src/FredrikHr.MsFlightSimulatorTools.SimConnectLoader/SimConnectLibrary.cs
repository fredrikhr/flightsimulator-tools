using System.Diagnostics;
using System.Reflection;

using Microsoft.FlightSimulator.SimConnect;

using SimConnectNativeLibraryHandle =
#if NETCOREAPP
    nint
#else // !NETCOREAPP
    Windows.Win32.FreeLibrarySafeHandle
#endif // !NETCOREAPP
    ;

namespace FredrikHr.MsFlightSimulatorTools.SimConnectLoader;

public static class SimConnectLibrary
{
    public const int SimConnectMessageId = 0x0402;

    private static readonly object syncLock = new();
    private static SimConnectNativeLibraryHandle unmanagedLibHandle = default!;
    private static Type? simconnectTypeRef;
    private static SimConnectAssemblyPath? assemblyPath;

    public const string SimConnectTypeName = "Microsoft.FlightSimulator.SimConnect.SimConnect";

    public static bool IsInitialized => simconnectTypeRef is not null &&
#if NETCOREAPP
        unmanagedLibHandle != default
#else // !NETCOREAPP
        unmanagedLibHandle is { IsInvalid: false }
#endif // !NETCOREAPP
        ;

    public static Task<IDisposable> CreateSimConnectAsync(
        string name,
        WaitHandle eventHandle,
        int configIndex = default,
        CancellationToken cancelToken = default
        )
    {
#if NETCOREAPP
        ArgumentNullException.ThrowIfNull(eventHandle);
#else // !NETCOREAPP
        _ = eventHandle ?? throw new ArgumentNullException(
            paramName: nameof(eventHandle)
            );
#endif // !NETCOREAPP
        return CreateSimConnectCoreAsync(
            name,
            eventHandle,
            hWnd: default,
            wmMessageId: default,
            configIndex,
            cancelToken
            );
    }

    public static Task<IDisposable> CreateSimConnectAsync(
        string name,
        nint hWnd,
        int messageId = SimConnectMessageId,
        int configIndex = default,
        CancellationToken cancelToken = default
        )
    {
        if (hWnd == default)
            throw new ArgumentNullException(
                paramName: nameof(hWnd)
                );
        return CreateSimConnectCoreAsync(
            name,
            eventHandle: null,
            hWnd,
            messageId,
            configIndex,
            cancelToken
            );
    }

    private static Task<IDisposable> CreateSimConnectCoreAsync(
        string name,
        WaitHandle? eventHandle,
        nint hWnd,
        int wmMessageId = SimConnectMessageId,
        int configIndex = default,
        CancellationToken cancelToken = default
        )
    {
        cancelToken.ThrowIfCancellationRequested();
        try
        {
            return Task.FromResult(CreateSimConnectCore(
                name,
                eventHandle,
                hWnd,
                wmMessageId,
                configIndex
                ));
        }
        catch (System.Runtime.InteropServices.COMException)
        {
            return DelayCreateSimConnectCoreAsync(
                name,
                eventHandle,
                hWnd,
                wmMessageId,
                configIndex,
                cancelToken
                );
        }
    }

    private static async Task<IDisposable> DelayCreateSimConnectCoreAsync(
        string name,
        WaitHandle? eventHandle,
        nint hWnd,
        int wmMessageId = SimConnectMessageId,
        int configIndex = default,
        CancellationToken cancelToken = default
        )
    {
        cancelToken.ThrowIfCancellationRequested();
        await Task.Delay(TimeSpan.FromSeconds(5), cancelToken)
            .ConfigureAwait(continueOnCapturedContext: false);
        cancelToken.ThrowIfCancellationRequested();
        try
        {
            return CreateSimConnectCore(
                name,
                eventHandle,
                hWnd,
                wmMessageId,
                configIndex
                );
        }
        catch (System.Runtime.InteropServices.COMException) { }
        return await DelayCreateSimConnectCoreAsync(
            name,
            eventHandle,
            hWnd,
            wmMessageId,
            configIndex,
            cancelToken
            )
            .ConfigureAwait(continueOnCapturedContext: false);
    }

    public static IDisposable CreateSimConnect(string name, nint hWnd, int messageId = SimConnectMessageId, int configIndex = default)
    {
        if (hWnd == default)
            throw new ArgumentNullException(
                paramName: nameof(hWnd)
                );
        return CreateSimConnectCore(
            name,
            eventHandle: null,
            hWnd,
            messageId,
            configIndex
            );
    }

    public static IDisposable CreateSimConnect(string name, WaitHandle eventHandle, int configIndex = default)
    {
#if NETCOREAPP
        ArgumentNullException.ThrowIfNull(eventHandle);
#else // !NETCOREAPP
        _ = eventHandle ?? throw new ArgumentNullException(
            paramName: nameof(eventHandle)
            );
#endif // !NETCOREAPP
        return CreateSimConnectCore(
            name,
            eventHandle: eventHandle,
            hWnd: default,
            wmMessageId: default,
            configIndex
            );
    }

    private static IDisposable CreateSimConnectCore(
        string name,
        WaitHandle? eventHandle,
        nint hWnd,
        int wmMessageId = SimConnectMessageId,
        int configIndex = default
        )
    {
        var simConnectTypeRef = EnsureSimConnectType();
        return CreateSimConnectCore(
            simConnectTypeRef,
            name,
            eventHandle,
            hWnd,
            wmMessageId,
            configIndex
            );
    }

    private static IDisposable CreateSimConnectCore(
        Type simconnectTypeRef,
        string name,
        WaitHandle? eventHandle,
        nint hWnd,
        int wmMessageId = SimConnectMessageId,
        int configIndex = default
        )
    {
        var simconnect = (IDisposable)Activator.CreateInstance(
            simconnectTypeRef,
            new object[]
            {
                name,
                hWnd,
                (uint)wmMessageId,
                eventHandle!,
                (uint)configIndex
            })!;
        return simconnect;
    }

    [Conditional("DEBUG")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051: Remove unused private members")]
    private static void CreateSimConnectCoreExample(
        string name,
        WaitHandle? eventHandle = default,
        nint hWnd = default,
        int userEventMessage = default,
        int configIndex = 0
        )
    {
        using SimConnect simConnect = new(
            name,
            hWnd,
            (uint)userEventMessage,
            eventHandle,
            (uint)configIndex
            );
    }

    public static void Initialize(SimConnectAssemblyPath assemblyPath)
    {
        if (IsInitialized) return;
        lock (syncLock)
        {
            SimConnectLibrary.assemblyPath = assemblyPath;
            LoadFromAssemblyPath(assemblyPath);
        }
    }

    private static Type EnsureSimConnectType()
    {
        Type? simconnectTypeRef;
        lock (syncLock)
        {
            simconnectTypeRef = SimConnectLibrary.simconnectTypeRef;
        }
        if (simconnectTypeRef is not null) return simconnectTypeRef;
        lock (syncLock)
        {
            simconnectTypeRef = SimConnectLibrary.simconnectTypeRef;
            if (simconnectTypeRef is not null) return simconnectTypeRef;
            simconnectTypeRef = LoadFromAssemblyPath(assemblyPath);
            SimConnectLibrary.simconnectTypeRef = simconnectTypeRef;
            return simconnectTypeRef;
        }
    }

    private static Type LoadFromAssemblyPath(SimConnectAssemblyPath? assemblyPath)
    {
        Type? simconnectTypeRef;
        if (assemblyPath is null)
            throw new InvalidOperationException(
                "SimConnect Library has not yes been initialized."
                );
#if NETCOREAPP
        unmanagedLibHandle = System.Runtime.InteropServices.NativeLibrary
            .Load(assemblyPath.UnmanagedLibraryFilePath);
#else // !NETCOREAPP
        unmanagedLibHandle = Windows.Win32.PInvoke
            .LoadLibrary(assemblyPath.UnmanagedLibraryFilePath);
#endif // !NETCOREAPP
        var managedAssemblyRef = Assembly.LoadFrom(
            assemblyPath.ManagedAssemblyFilePath
            );
        simconnectTypeRef = Type.GetType(
            $"{SimConnectTypeName}, {managedAssemblyRef.FullName ?? assemblyPath.ManagedAssemblyName.ToString()}",
            throwOnError: true,
            ignoreCase: true
            )!;
        return simconnectTypeRef;
    }
}
