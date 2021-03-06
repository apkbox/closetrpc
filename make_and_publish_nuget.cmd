@echo off
setlocal

if not exist nuget.exe (
	echo error: Missing nuget.exe. Download nuget.exe and place in the project root.
	goto end
)

set BASE_PATH=%~dp0
set OUTPUT_DIR=%BASE_PATH%\build
set NUGET_PACKAGE_DIR=%OUTPUT_DIR%\packages
set NUGET_LOCAL_FEED=%1
set BUILD_CONFIGURATION=Debug

rem msbuild /m /p:Configuration=Release /p:Platform="Any CPU" "%BASE_PATH%\closetrpc_dotnet.sln"

rmdir /s/q %NUGET_PACKAGE_DIR%

nuget pack "%BASE_PATH%\dotnet\ClosetRpc.Net" -Properties Configuration=%BUILD_CONFIGURATION% -OutputDirectory "%NUGET_PACKAGE_DIR%" -build -symbols -IncludeReferencedProjects
nuget pack "%BASE_PATH%\dotnet\ClosetRpc.Net.Protobuf" -Properties Configuration=%BUILD_CONFIGURATION% -OutputDirectory "%NUGET_PACKAGE_DIR%" -build -symbols -IncludeReferencedProjects
nuget pack "%BASE_PATH%\dotnet\ClosetRpc.Net.Util" -Properties Configuration=%BUILD_CONFIGURATION% -OutputDirectory "%NUGET_PACKAGE_DIR%" -build -symbols -IncludeReferencedProjects

if "%NUGET_LOCAL_FEED%"=="" (
	echo.
	echo ===================================================================
	echo.
	echo warning: Missing NuGet local feed location, skipping publishing step
	echo.
	echo ===================================================================
	echo.
) else (
	for %%n in (%NUGET_PACKAGE_DIR%\*.nupkg) do nuget add %%n -source %NUGET_LOCAL_FEED%\
)

:end
endlocal
