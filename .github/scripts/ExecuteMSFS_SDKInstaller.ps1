[CmdletBinding()]
param ()

$MsiExecCmd = Get-Command msiexec -ErrorAction Stop
$MsiPath = Resolve-Path ".\msi\MSFS_SDK_Core_Installer.msi"
$MsiLogFile = Join-Path "msi" "MSFS_SDK_Core_Installer.log"
Start-Process $MsiExecCmd `
    "/i", $MsiPath, "/quiet", "/l*", $MsiLogFile `
    -NoNewWindow -Wait `
    -Verbose:($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue)
Get-Content ".\msi\MSFS_SDK_Core_Installer.log" | Write-Host
