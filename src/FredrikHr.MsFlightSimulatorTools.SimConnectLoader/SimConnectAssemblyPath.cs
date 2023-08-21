using System.Reflection;

namespace FredrikHr.MsFlightSimulatorTools.SimConnectLoader;

public class SimConnectAssemblyPath
{
    private const string unmanagedDllFileName = "SimConnect.dll";
    private const string managedAssemblyFileName = "Microsoft.FlightSimulator.SimConnect.dll";

    public string UnmanagedLibraryDirectoryPath { get; }
    public string UnmanagedLibraryFilePath { get; }
    public string ManagedAssemblyDirectoryPath { get; }
    public string ManagedAssemblyFilePath { get; }
    public string UnmanagedLibraryName { get; }
    public AssemblyName ManagedAssemblyName { get; }

    public SimConnectAssemblyPath(string unmanagedLibDirPath, string managedLibDirPath) : base()
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(unmanagedLibDirPath);
        ArgumentNullException.ThrowIfNull(managedLibDirPath);
#else
        _ = unmanagedLibDirPath ?? throw new ArgumentNullException(nameof(unmanagedLibDirPath));
        _ = managedLibDirPath ?? throw new ArgumentNullException(nameof(managedLibDirPath));
#endif
        if (!Directory.Exists(unmanagedLibDirPath)) {
            throw new DirectoryNotFoundException(
                $"The specified path '{unmanagedLibDirPath}' that is to be used for the directory containing the native SimConnect library does not exist or is not a valid directory."
            );
        }
        if (!Directory.Exists(managedLibDirPath)) {
            throw new DirectoryNotFoundException(
                $"The specified path '{managedLibDirPath}' that is to be used for the directory containing the managed .NET SimConnect library does not exist or is not a valid directory."
            );
        }

        UnmanagedLibraryDirectoryPath = unmanagedLibDirPath;
        UnmanagedLibraryFilePath = Path.Combine(
            UnmanagedLibraryDirectoryPath, unmanagedDllFileName);
        ManagedAssemblyDirectoryPath = managedLibDirPath;
        ManagedAssemblyFilePath = Path.Combine(
            ManagedAssemblyDirectoryPath,
            managedAssemblyFileName);
        UnmanagedLibraryName = Path.GetFileNameWithoutExtension(UnmanagedLibraryFilePath);
        ManagedAssemblyName = new(Path.GetFileNameWithoutExtension(managedAssemblyFileName));
    }

    public static SimConnectAssemblyPath FromMsFsSdkEnvironmentVariable()
    {
        var msfsSdk = Environment.GetEnvironmentVariable("MSFS_SDK") ??
            throw new DirectoryNotFoundException(
                "The MSFS_SDK environment variable is not set"
                );
        return FromMsFsSdkPath(msfsSdk);
    }

    public static SimConnectAssemblyPath FromMsFsSdkPath(string msfsSdk)
    {
#if NETCOREAPP
        ArgumentException.ThrowIfNullOrEmpty(msfsSdk);
#else
        if (string.IsNullOrEmpty(msfsSdk))
        {
            throw msfsSdk is null
                ? throw new ArgumentNullException(nameof(msfsSdk))
                : throw new ArgumentException(
                    paramName: nameof(msfsSdk),
                    message: "The specified path for the MSFS SDK must not be an empty string."
                    );
        }
#endif
        if (!Directory.Exists(msfsSdk))
        {
            throw new DirectoryNotFoundException(
                $"The specified path '{msfsSdk}' does not exist or does not point to a valid directory."
                );
        }
        var nativeLibDirPath = Path.Combine(msfsSdk, "SimConnect SDK", "lib");
        var managedLibDirPath = Path.Combine(nativeLibDirPath, "managed");
        return new(nativeLibDirPath, managedLibDirPath);
    }
}
