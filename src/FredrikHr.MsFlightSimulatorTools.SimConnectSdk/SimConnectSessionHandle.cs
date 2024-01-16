using Microsoft.Win32.SafeHandles;

using Windows.Win32.Foundation;

namespace FredrikHr.MsFlightSimulatorTools.SimConnectSdk;

public class SimConnectSessionHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public SimConnectSessionHandle() : base(ownsHandle: true) { }

    protected override bool ReleaseHandle()
    {
        int retval = SimConnectNativeMethods.SimConnect_Close(handle);
        HRESULT hr = new(retval);
        return hr.Succeeded;
    }
}
