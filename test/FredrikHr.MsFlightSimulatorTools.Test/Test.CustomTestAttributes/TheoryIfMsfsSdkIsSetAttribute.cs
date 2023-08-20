namespace FredrikHr.MsFlightSimulatorTools.Test.CustomTestAttributes;

internal sealed class TheoryIfMsfsSdkIsSetAttribute : TheoryAttribute
{
    public TheoryIfMsfsSdkIsSetAttribute() : base()
    {
        if (Environment.GetEnvironmentVariable("MSFS_SDK") is not { Length: > 0 })
            Skip = "MSFS_SDK environment variable is not set";
    }
}
