[CmdletBinding()]
param (
    [Parameter(Mandatory = $true)]
    [string]$Version,
    [Parameter()]
    [string]$LongVersion = "${Version}.0",
    [Parameter()]
    [uri]$DownloadUrl = "https://sdk.flightsimulator.com/files/installers/${Version}/MSFS_SDK_Core_Installer_${LongVersion}.msi"
)

if (-not (Test-Path -PathType Container "msi")) {
    [void](New-Item -ItemType Directory "msi" -Verbose:($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue))
}

$TargetFilePath = Join-Path "msi" "MSFS_SDK_Core_Installer.msi"

if ($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue) {
    Write-Verbose $DownloadUrl
}
Invoke-WebRequest -Uri $DownloadUrl -OutFile $TargetFilePath `
    -Verbose:($VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue)

$TargetFilePath = Resolve-Path $TargetFilePath
$GitHubActionsOutputString = "msi-installer-path=${TargetFilePath}"
if ($ENV:GITHUB_OUTPUT) {
    $GitHubActionsOutputString | Out-File -Append -LiteralPath $ENV:GITHUB_OUTPUT -Verbose:$IsScriptVerbose
}
else {
    Write-Verbose $GitHubActionsOutputString
}
