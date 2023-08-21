using System.Runtime.InteropServices;

using Microsoft.FlightSimulator.SimConnect;

namespace FredrikHr.MsFlightSimulatorTools.SimConnectLoader;

public sealed class SimConnectApplication : IDisposable
{
    public const string SimConnectTypeName = "Microsoft.FlightSimulator.SimConnect.SimConnect";

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079: Remove unnecessary suppression")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052: Remove unread private members", Justification = nameof(System.Diagnostics))]
    private readonly bool ownsWaitHandle;
    private readonly nint unmanagedLibHandle;
    private readonly IDisposable simconnect = null!;

    public WaitHandle? WaitHandle { get; }
    public Thread? MessagePollingThread { get; private set; }
    public SimConnect SimConnect => (SimConnect)simconnect;

    private SimConnectApplication(
        SimConnectAssemblyPath simconnectPath,
        string name,
        IntPtr hWnd,
        uint win32MsgId,
        WaitHandle? waitHandle,
        int confixIdx,
        bool ownsWaitHandle
        ) : base()
    {
#if NETCOREAPP
        ArgumentNullException.ThrowIfNull(simconnectPath);
#else
        _ = simconnectPath ?? throw new ArgumentNullException(nameof(simconnectPath));
#endif
        unmanagedLibHandle = LoadUnmanagedLibrary(simconnectPath);
        var hSimConnect = Activator.CreateInstanceFrom(
            simconnectPath.ManagedAssemblyFilePath,
            SimConnectTypeName,
            ignoreCase: true,
            bindingAttr: default,
            Type.DefaultBinder,
            new object?[]
            {
                name, hWnd, win32MsgId,
                waitHandle, (uint)confixIdx,
            },
            System.Globalization.CultureInfo.InvariantCulture,
            activationAttributes: default
            )!;
        simconnect = (IDisposable)hSimConnect.Unwrap()!;
        WaitHandle = waitHandle;
        this.ownsWaitHandle = ownsWaitHandle;
    }

    public SimConnectApplication(
        SimConnectAssemblyPath simconnectPath,
        string name,
        int configIdx = default
        ) : this(simconnectPath, name,
            System.Diagnostics.Process.GetCurrentProcess().Handle,
            win32MsgId: default,
            new AutoResetEvent(initialState: false),
            configIdx,
            ownsWaitHandle: true
            ) { }

    public SimConnectApplication(
        SimConnectAssemblyPath simconnectPath,
        string name,
        IntPtr hWnd, uint win32MsgId,
        int configIdx = default
        ) : this(simconnectPath, name, hWnd, win32MsgId, null, configIdx, ownsWaitHandle: false) { }

    public SimConnectApplication(
        SimConnectAssemblyPath simconnectPath, string name,
        WaitHandle? eventHandle,
        int configIdx = default,
        IntPtr hWnd = default, uint win32MsgId = default
        ) : this(simconnectPath, name, hWnd, win32MsgId, eventHandle, configIdx, ownsWaitHandle: false) { }

    public void NotifyMessageAvailable()
    {
        if (MessagePollingThread?.IsAlive == true) return;
        SimConnect.ReceiveMessage();
    }

    public void ReceiveDispatch(SignalProcDelegate messageReceiver)
    {
        if (MessagePollingThread?.IsAlive == true)
            throw new InvalidOperationException(
                "Manually receiving SimConnect data is disabled while a message polling thread is running."
                );
        SimConnect.ReceiveDispatch(messageReceiver);
    }

    public void RunMessagePollingThread()
    {
        if (MessagePollingThread?.IsAlive == true) return;
        if (WaitHandle is null)
            throw new InvalidOperationException(
                "Message polling thread is not available when SimConnectApplication is initialized without a Wait Handle."
                );
        Thread th = new(new ParameterizedThreadStart(MessagePolling))
        { IsBackground = true };
        MessagePollingThread = th;
        th.Start(this);
    }

    private static void MessagePolling(object? threadArgsObj)
    {
        var app = (SimConnectApplication?)threadArgsObj;
        var waitHandle = app?.WaitHandle;
        var simconnect = app?.SimConnect;

        if (waitHandle is null) return;
        if (simconnect is null) return;

        try
        {
            while (waitHandle.WaitOne())
            {
                simconnect.ReceiveMessage();
            }
        }
        catch (ObjectDisposedException)
        {
            return;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        simconnect?.Dispose();
        if (ownsWaitHandle) WaitHandle?.Dispose();
    }

    ~SimConnectApplication()
    {
        Dispose();
    }

    public bool IsAppSimConnect(SimConnect simconnect) =>
        ReferenceEquals(this.simconnect, simconnect);

    private static nint LoadUnmanagedLibrary(SimConnectAssemblyPath simconnectPath)
    {
#if NETCOREAPP
        nint unmanagedLibHandle = NativeLibrary.Load(simconnectPath.UnmanagedLibraryFilePath);
        return unmanagedLibHandle;
#else
        IntPtr unmanagedLibHandle = PInvoke(simconnectPath.UnmanagedLibraryFilePath);
        if (unmanagedLibHandle == IntPtr.Zero)
        {
            var unmanagedLibError = Marshal.GetLastWin32Error();
            throw new System.ComponentModel.Win32Exception(unmanagedLibError);
        }
        return unmanagedLibHandle;

        [DllImport("KERNEL32.dll", ExactSpelling = true, EntryPoint = "LoadLibraryW", SetLastError = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        static extern IntPtr PInvoke([MarshalAs(UnmanagedType.LPWStr)] string lpLibFileName);
#endif
    }
}
