using System.Reflection;

using FredrikHr.MsFlightSimulatorTools.Test.CustomTestAttributes;

namespace FredrikHr.MsFlightSimulatorTools.SimConnectLoader.Test;

public static class SimConnectAssemblyPathTest
{
    [Fact]
    public static void New_without_directory_path_throws_Argument()
    {
        Assert.Throws<ArgumentNullException>(
            () => new SimConnectAssemblyPath(null!, null!)
            );
    }

    [Fact]
    public static void New_with_empty_directory_path_throws_DirectoryNotFound()
    {
        Assert.Throws<DirectoryNotFoundException>(
            () => new SimConnectAssemblyPath(string.Empty, string.Empty)
            );
    }

    [Fact]
    public static void New_with_invalid_directory_path_throws_DirectoryNotFound()
    {
        string msfsSdkPath = Path.Combine("Path", "to", "directory", "that does not exist");
        Assert.Throws<DirectoryNotFoundException>(
            () => SimConnectAssemblyPath.FromMsFsSdkPath(msfsSdkPath)
            );
    }

    [FactIfMsfsSdkSet]
    public static void FromMsFsSdkVariable_succeeds()
    {
        var simconnectPath = SimConnectAssemblyPath.FromMsFsSdkEnvironmentVariable();
        Assert.True(File.Exists(simconnectPath.ManagedAssemblyFilePath));
        Assert.True(File.Exists(simconnectPath.UnmanagedLibraryFilePath));
    }
}
