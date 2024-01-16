[CmdletBinding()]
param ()

$MsiExecCmd = Get-Command msiexec -ErrorAction Stop
$MsiPath = Resolve-Path ".\msi\MSFS_SDK_Core_Installer.msi"
$MsiLogFile = Join-Path "msi" "MSFS_SDK_Core_Installer.log"
$MsiExecProcess = Start-Process $MsiExecCmd `
    "/i", $MsiPath, "/quiet", "/l*", $MsiLogFile `
    -NoNewWindow -Wait `
    -Verbose:($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue)
Get-Content ".\msi\MSFS_SDK_Core_Installer.log" | Write-Host
[ValidateNotNullOrEmpty()][string]$PostInstallMsFsSdkVariable = [System.Environment]::GetEnvironmentVariable(
    "MSFS_SDK",
    [System.EnvironmentVariableTarget]::Machine
)
"MSFS_SDK=${PostInstallMsFsSdkVariable}" | Out-File -Append $ENV:GITHUB_OUTPUT
exit $MsiExecProcess.ExitCode
