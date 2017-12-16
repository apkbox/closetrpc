@echo off
setlocal

set BASE_PATH=%~dp0


%BASE_PATH%\third_party\protobuf\bin\protoc ^
  --proto_path=%BASE_PATH%\shared ^
  %BASE_PATH%\shared\codegen_test.proto ^
  --cpp_out=%BASE_PATH%\shared\ ^
  --csharp_out=%BASE_PATH%\dotnet\CodeGenerationTemplate\

endlocal

