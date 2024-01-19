#Requires -Version 6.0
[CmdletBinding()]
param ()

[switch]$IsScriptVerbose = $VerbosePreference -ne [System.Management.Automation.ActionPreference]::SilentlyContinue

$KnownVersionsFileName = "MSFS_SDK.known-versions.json"
$KnownVersionsDirPath = Join-Path -Resolve $PSScriptRoot (Join-Path ".." "..")
$KnownVersionsFilePath = Join-Path -Resolve $KnownVersionsDirPath $KnownVersionsFileName

Remove-Variable "CoreInstallerWebSession" -EA SilentlyContinue
[PSObject[]]$KnownVersionsMatrixItems = Get-Content -LiteralPath $KnownVersionsFilePath `
| ConvertFrom-Json `
| ForEach-Object {
    [string]$KnownVersionString = $_
    $KnownVersionLongString = "${KnownVersionString}.0"
    [string]$CoreInstallerUrl = "https://sdk.flightsimulator.com/files/installers/${KnownVersionString}/MSFS_SDK_Core_Installer_${KnownVersionLongString}.msi"
    if ($IsScriptVerbose) {
        Write-Verbose "$CoreInstallerUrl"
    }
    Remove-Variable "CoreInstallerHttpHeaders", "CoreInstallerHttpStatus", "CoreInstallerDate" -EA SilentlyContinue
    [void](Invoke-RestMethod -Method Head $CoreInstallerUrl `
            -SkipHttpErrorCheck `
            -ResponseHeadersVariable "CoreInstallerHttpHeaders" `
            -StatusCodeVariable "CoreInstallerHttpStatus" `
            -SessionVariable "CoreInstallerWebSession" `
            -Verbose:$IsScriptVerbose
    )
    if ($IsScriptVerbose) {
        Write-Verbose "HTTP Status code: ${CoreInstallerHttpStatus}"
        Write-Verbose ""
    }
    [bool]$CoreInstallerAvailable = ([int]($CoreInstallerHttpStatus) -ge 200) `
        -and ([int]($CoreInstallerHttpStatus) -lt 300)
    if ($CoreInstallerAvailable) {
        if ($CoreInstallerHttpHeaders.ContainsKey("Last-Modified")) {
            [string]$CoreInstallerDate = $CoreInstallerHttpHeaders.'Last-Modified' `
            | ForEach-Object { [datetime]$_ } `
            | Select-Object -First 1
            | ForEach-Object { $_.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'hh':'mm':'ss'Z'", [cultureinfo]::InvariantCulture) }
        }
        New-Object PSObject -Property ([ordered]@{
                version        = $KnownVersionString
                "long-version" = $KnownVersionLongString
                "msi-url"      = $CoreInstallerUrl
                "msi-date"     = $CoreInstallerDate
            })
    }
    else {
        Write-Warning "MSFS SDK Core Installer v${KnownVersionString} not available."
    }
}
$KnownVersionsMatrixObject = New-Object PSObject -Property @{
    include = $KnownVersionsMatrixItems
}
[string]$KnownVersionsMatrixString = ConvertTo-Json -Compress -InputObject $KnownVersionsMatrixObject
$GitHubActionsOutputString = "msfs-sdk-matrix=${KnownVersionsMatrixString}"
if ($ENV:GITHUB_OUTPUT) {
    $GitHubActionsOutputString | Out-File -Append -LiteralPath $ENV:GITHUB_OUTPUT -Verbose:$IsScriptVerbose
}
else {
    Write-Verbose $GitHubActionsOutputString
}
