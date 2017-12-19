@echo off
setlocal

set BASE_PATH=%~dp0

%BASE_PATH%..\..\third_party\protobuf\bin\protoc ^
  --proto_path=%BASE_PATH%..\..\shared ^
  --proto_path=%BASE_PATH% ^
  %BASE_PATH%\services.proto ^
  --csharp_out=%BASE_PATH% ^
  --plugin=protoc-gen-nanorpc_csharp=%BASE_PATH%..\..\bin\x64\Debug\closetrpc_csharp_plugin.exe ^
  --nanorpc_csharp_out=%BASE_PATH%

endlocal

