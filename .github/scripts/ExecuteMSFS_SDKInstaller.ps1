[CmdletBinding()]
param (
    [Parameter()]
    [string]$MsiPath = (Resolve-Path ".\msi\MSFS_SDK_Core_Installer.msi")
)

$MsiExecCmd = Get-Command msiexec -ErrorAction Stop
if (-not (Test-Path -PathType Container "msi")) {
    [void](New-Item -ItemType Directory "msi" -Verbose:($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue))
}
$MsiLogFile = Join-Path "msi" "MSFS_SDK_Core_Installer.log"
$MsiExecProcess = Start-Process $MsiExecCmd `
    "/i", (Resolve-Path $MsiPath -EA Stop), "/quiet", "/l*", $MsiLogFile `
    -NoNewWindow -Wait `
    -Verbose:($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue)
Get-Content ".\msi\MSFS_SDK_Core_Installer.log" | Write-Host

[ValidateNotNullOrEmpty()][string]$MsfsSdkInstallPath = [System.Environment]::GetEnvironmentVariable(
    "MSFS_SDK",
    [System.EnvironmentVariableTarget]::Machine
)
$GitHubActionsEnvString = "MSFS_SDK=${MsfsSdkInstallPath}"
if ($ENV:GITHUB_ENV) {
    $GitHubActionsEnvString | Out-File -Append -LiteralPath $ENV:GITHUB_ENV -Verbose:$IsScriptVerbose
}
else {
    Write-Verbose $GitHubActionsEnvString
}

exit $MsiExecProcess.ExitCode
