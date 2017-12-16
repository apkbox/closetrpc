@echo off
if "%1"=="" ( 
	echo usage: %~n0 protobuf_dir [-make] [-fetch]
	echo.
	echo     -fetch implies -make
	exit /b 0
)

setlocal
set _PROTOBUF_PUBLISH_DIR=%~dp0\third_party\protobuf
set _PROTOBUF_DIR=%1
set _MAKE_PB=0
set _FETCH_PB=-fetch

:next_arg
shift

if "%1" EQU "" goto resume

if "%1" EQU "-make" (
	set _MAKE_PB=1
	set _FETCH_PB=-fetch
) else if "%1" EQU "-fetch" (
	set _FETCH_PB=-fetch
) else (
	echo error: Invalid command line parameter.
	exit /b 1
)

goto next_arg

:resume

if %_MAKE_PB%==1 call make_protobuf.cmd %_PROTOBUF_DIR% %_FETCH_PB%
if errorlevel 1 exit /b 1

echo Publishing...
if exist "%_PROTOBUF_PUBLISH_DIR%" (
	move "%_PROTOBUF_PUBLISH_DIR%" "%_PROTOBUF_PUBLISH_DIR%.old"
)

xcopy /s/e/y "%_PROTOBUF_DIR%\cmake\build\install" "%_PROTOBUF_PUBLISH_DIR%\"

if exist "%_PROTOBUF_PUBLISH_DIR%.old" (
	rmdir /s/q "%_PROTOBUF_PUBLISH_DIR%.old"
)

xcopy /s/e/y "%_PROTOBUF_DIR%\csharp\src\Google.Protobuf\bin" "%_PROTOBUF_PUBLISH_DIR%_dotnet\bin\"

:end
endlocal
exit /b 0

