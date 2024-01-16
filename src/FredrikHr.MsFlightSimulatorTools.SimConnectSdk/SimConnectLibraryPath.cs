using System.Runtime.InteropServices;

using Windows.Win32.Foundation;

namespace FredrikHr.MsFlightSimulatorTools.SimConnectSdk;

public static class SimConnectLibraryPath
{
    private static readonly object _lock = new();
    private static readonly unsafe Dictionary<string, nint> _cookies =
        new(StringComparer.OrdinalIgnoreCase);

    public static unsafe void RegisterDirectory(string path)
    {
        path = Path.GetFullPath(path);
        path = Path.TrimEndingDirectorySeparator(path);
        var cookiePtr = Windows.Win32.PInvoke.AddDllDirectory(path);
        if (cookiePtr == null)
        {
            HRESULT hr = new(Marshal.GetLastWin32Error());
            hr.ThrowOnFailure();
            return;
        }
        nint cookie = new(cookiePtr);
        lock (_lock)
        {
            _cookies[path] = cookie;
        }
    }

    public static unsafe bool UnregisterDirectory(string path)
    {
        path = Path.GetFullPath(path);
        path = Path.TrimEndingDirectorySeparator(path);
        nint cookie;
        lock (_lock)
        {
            if (!_cookies.TryGetValue(path, out cookie))
                return false;
            _cookies.Remove(path);
        }
        void* cookiePtr = cookie.ToPointer();
        bool retval = Windows.Win32.PInvoke.RemoveDllDirectory(cookiePtr);
        if (!retval)
        {
            HRESULT hr = new(Marshal.GetLastSystemError());
            hr.ThrowOnFailure();
        }
        return retval;
    }
}
