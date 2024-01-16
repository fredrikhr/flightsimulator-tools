namespace FredrikHr.MsFlightSimulatorTools.SimConnectSdk;

public class MsFsSdkSimConnectLocator
{
    public const string EnvironmentVariableName = "MSFS_SDK";

    public static MsFsSdkSimConnectLocator FromEnvironmentVariable()
    {
        static string? GetMsfsSdkEnvironmentVariable() =>
            Environment.GetEnvironmentVariable(EnvironmentVariableName);
        return new(GetMsfsSdkEnvironmentVariable);
    }

    public static MsFsSdkSimConnectLocator FromDirectoryPath(string directoryPath)
    {
        return new(() => directoryPath);
    }

    private readonly Func<string?> pathRetriever;

    private MsFsSdkSimConnectLocator(Func<string?> pathRetriever) : base()
    {
        this.pathRetriever = pathRetriever;
    }

    public string? GetSimConnectDirectoryPath()
    {
        string? msfssdkPath = pathRetriever();
        if (string.IsNullOrEmpty(msfssdkPath)) return default;
        string simConnectPath = Path.Combine(
            msfssdkPath,
            "SimConnect SDK",
            "lib"
            );
        return simConnectPath;
    }
}
