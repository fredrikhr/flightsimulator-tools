[CmdletBinding()]
param ()

[xml]$MsfsSdkPropsDocument = Get-Content "MSFS_SDK.Version.props"
[ValidateNotNullOrEmpty()][string]$MSFS_SDKVersion = $MsfsSdkPropsDocument.Project.PropertyGroup.MSFS_SDKVersion
[ValidateNotNullOrEmpty()][string]$MSFS_SDKFileVersion = $MsfsSdkPropsDocument.Project.PropertyGroup.MSFS_SDKFileVersion
[uri]$MSFSSdkDownloadUrl = "https://sdk.flightsimulator.com/files/installers/${MSFS_SDKVersion}/MSFS_SDK_Core_Installer_${MSFS_SDKFileVersion}.msi"

if (-not (Test-Path -PathType Container "msi")) {
    [void](New-Item -ItemType Directory "msi" -Verbose:($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue))
}
if ($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue) {
    Write-Verbose $MSFSSdkDownloadUrl
}

$TargetFilePath = Join-Path "msi" "MSFS_SDK_Core_Installer.msi"

Invoke-WebRequest -Uri $MSFSSdkDownloadUrl -OutFile $TargetFilePath `
    -Verbose:($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue)
