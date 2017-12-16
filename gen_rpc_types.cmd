@echo off
setlocal

set BASE_PATH=%~dp0
set _SRC_PATH=%BASE_PATH%\shared


%BASE_PATH%\third_party\protobuf\bin\protoc ^
  "--proto_path=%_SRC_PATH%" ^
  "%_SRC_PATH%\closetrpc_types.proto" ^
  --cpp_out=%_SRC_PATH%\ ^
  --csharp_out=%BASE_PATH%\dotnet\ClosetRpc.Net.Protobuf

endlocal
