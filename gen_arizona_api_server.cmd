@echo off
setlocal

set BASE_PATH=%~dp0


%BASE_PATH%\third_party\protobuf\bin\protoc ^
  --proto_path=%BASE_PATH%\shared ^
  --proto_path=%BASE_PATH%\examples\arizona_api_server\src\ ^
  %BASE_PATH%\examples\arizona_api_server\src\arizona_api_server.proto ^
  --cpp_out=%BASE_PATH%\examples\arizona_api_server\src\ ^
  --csharp_out=%BASE_PATH%\examples\arizona_api_server\src\ ^
  --plugin=protoc-gen-nanorpc_cpp=%BASE_PATH%\bin\x64\Debug\closetrpc_cpp_plugin.exe ^
  --plugin=protoc-gen-nanorpc_csharp=%BASE_PATH%\bin\x64\Debug\closetrpc_csharp_plugin.exe ^
  --nanorpc_cpp_out=%BASE_PATH%\examples\arizona_api_server\src\ ^
  --nanorpc_csharp_out=%BASE_PATH%\examples\arizona_api_server\src\

endlocal

