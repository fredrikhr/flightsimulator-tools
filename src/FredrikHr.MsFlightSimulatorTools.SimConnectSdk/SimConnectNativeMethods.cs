using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using Microsoft.Win32.SafeHandles;

using Windows.Win32.Foundation;

namespace FredrikHr.MsFlightSimulatorTools.SimConnectSdk;

public static partial class SimConnectNativeMethods
{
    public const string SimConnect_LibraryName = "SimConnect.dll";

    [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
    [LibraryImport(SimConnect_LibraryName, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
    private static partial int SimConnect_Open(
        out SimConnectSessionHandle hSimConnect,
        string name,
        nint hWnd,
        int msgUserEvent,
        nint hEventHandle,
        uint configIndex
        );

    [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
    [LibraryImport(SimConnect_LibraryName, StringMarshallingCustomType = typeof(AnsiStringMarshaller))]
    private static partial int SimConnect_Open(
        out SimConnectSessionHandle hSimConnect,
        string name,
        nint hWnd,
        int msgUserEvent,
        SafeWaitHandle hEventHandle,
        uint configIndex
        );

    public static SimConnectSessionHandle SimConnect_Open(
        string name,
        nint hWnd,
        int msgUserEvent,
        uint configIndex
        )
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentOutOfRangeException.ThrowIfEqual(hWnd, default);
        ArgumentOutOfRangeException.ThrowIfZero(msgUserEvent);
        int retval = SimConnect_Open(
            out var hSimConnect,
            name,
            hWnd,
            msgUserEvent,
            default(nint),
            configIndex);
        HRESULT hr = new(retval);
        hr.ThrowOnFailure();
        return hSimConnect;
    }

    public static SimConnectSessionHandle SimConnect_Open(
        string name,
        WaitHandle hEventHandle,
        uint configIndex
        )
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(hEventHandle);
        int retval = SimConnect_Open(
            out var hSimConnect,
            name,
            default,
            default,
            hEventHandle.SafeWaitHandle,
            configIndex);
        HRESULT hr = new(retval);
        hr.ThrowOnFailure();
        return hSimConnect;
    }

    [DefaultDllImportSearchPaths(DllImportSearchPath.UserDirectories)]
    [LibraryImport(SimConnect_LibraryName)]
    internal static partial int SimConnect_Close(
        nint hSimConnect
        );
}
