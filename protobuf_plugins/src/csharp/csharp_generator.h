#if !defined(NANORPC_COMPILER_CPP_CPP_GENERATOR_H__)
#define NANORPC_COMPILER_CPP_CPP_GENERATOR_H__

#include <string>
#include <vector>

namespace code_model {
class MethodModel;
class ServiceModel;
}  // namespace code_model

namespace google {
namespace protobuf {
class FileDescriptor;
}  // namespace protobuf
}  // namespace google

// csharp_generator.cpp
std::string GetMethodSignature(const code_model::MethodModel &method,
                               const std::string &service_name,
                               bool server);
std::string GetSourcePrologue(const ::google::protobuf::FileDescriptor *file);
std::string GetSourceEpilogue(const ::google::protobuf::FileDescriptor *file);

// csharp_interface_generator.cpp
std::string GetInterfaceDefinitions(
    const std::vector<code_model::ServiceModel> &models);

// csharp_proxy_generator.cpp
std::string GetProxyDeclarations(
    const std::vector<code_model::ServiceModel> &models);
std::string GetProxyDefinitions(
    const std::vector<code_model::ServiceModel> &models);

// csharp_stub_generator.cpp
std::string GetStubDefinitions(
    const ::google::protobuf::FileDescriptor *file,
    const std::vector<code_model::ServiceModel> &models);

#endif  // NANORPC_COMPILER_CPP_CPP_GENERATOR_H__
