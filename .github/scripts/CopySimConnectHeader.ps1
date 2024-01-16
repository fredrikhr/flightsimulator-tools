[CmdletBinding()]
param ()
$HeaderFileName = "SimConnect.h"
$TargetFolderPath = Join-Path -Resolve "abi" "SimConnect"
$TargetFilePath = Join-Path $TargetFolderPath $HeaderFileName

"#include <Windows.h>" | Out-File $TargetFilePath -Force -Encoding utf8NoBOM
[ValidateNotNullOrEmpty()][string]$MsfsSdkPath = $ENV:MSFS_SDK
$SimConnectSdkRootPath = Join-Path -Resolve $MsfsSdkPath "SimConnect SDK"
$SimConnectLibPath = Join-Path $SimConnectSdkRootPath "include"
$SimConnectHeaderFile = Join-Path $SimConnectLibPath $HeaderFileName

Get-Content $SimConnectHeaderFile | Out-File -Append $TargetFilePath -Encoding utf8NoBOM
