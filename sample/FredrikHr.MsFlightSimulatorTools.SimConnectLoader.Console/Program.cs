using FredrikHr.MsFlightSimulatorTools.SimConnectLoader;

using Microsoft.FlightSimulator.SimConnect;

internal static class Program
{
    private static void Main()
    {
        var path = SimConnectAssemblyPath.FromMsFsSdkEnvironmentVariable();
        using SimConnectApplication app = new(
            path,
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Name!
            );
        Console.CancelKeyPress += (sender, args) =>
        {
            app.WaitHandle?.Close();
        };
        RunSimConnectApplication(app);
    }

    private static void RunSimConnectApplication(SimConnectApplication app)
    {
        app.SimConnect.OnRecvOpen += SimConnect_OnRecvOpen;
        app.SimConnect.OnRecvQuit += SimConnect_OnRecvQuit;
        app.SimConnect.OnRecvSystemState += SimConnect_OnRecvSystemState;

        app.RunMessagePollingThread();

        app.SimConnect.RequestSystemState(SystemStateRequest.Sim, nameof(SystemStateRequest.Sim));
        app.SimConnect.RequestSystemState(SystemStateRequest.FlightPlan, nameof(SystemStateRequest.FlightPlan));

        Thread.Sleep(TimeSpan.FromSeconds(1));
        Console.WriteLine("Press any key to exit . . .");
        Console.ReadKey(intercept: true);

        void SimConnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Console.WriteLine("Connected to SimConnect");
            Console.WriteLine($"  Version: {data.dwSimConnectVersionMajor}.{data.dwSimConnectVersionMinor}.{data.dwSimConnectBuildMajor}.{data.dwSimConnectBuildMinor}");
            Console.WriteLine("Connected to Flight Simulator Application");
            Console.WriteLine($"  Name:    {data.szApplicationName}");
            Console.WriteLine($"  Version: {data.dwApplicationVersionMajor}.{data.dwApplicationVersionMinor}.{data.dwApplicationBuildMajor}.{data.dwApplicationBuildMinor}");
        }

        void SimConnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            var type = (SIMCONNECT_RECV_ID)data.dwID;
            Console.WriteLine($"Received SimConnect {type} message");
            Console.WriteLine("  Disconnected");
            app.WaitHandle?.Close();
        }

        void SimConnect_OnRecvSystemState(SimConnect sender, SIMCONNECT_RECV_SYSTEM_STATE data)
        {
            var type = (SIMCONNECT_RECV_ID)data.dwID;
            Console.WriteLine($"Received SimConnect {type} message");
            var requestId = (SystemStateRequest)data.dwRequestID;
            Console.WriteLine($"  Request ID:    {requestId}");
            Console.WriteLine($"  Integer value: {data.dwInteger}");
            Console.WriteLine($"  Float value:   {data.fFloat}");
            Console.WriteLine($"  String value:  {data.szString}");
        }
    }
}

enum SystemStateRequest
{
    Sim,
    FlightPlan
}
