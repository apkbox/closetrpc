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

const std::string kRpcNamespace = "global::ClosetRpc.Net";
const std::string kServiceBaseType = "global::ClosetRpc.Net.IRpcService";
const std::string kServerContextType = "global::ClosetRpc.Net.ServerContext";
const std::string kRpcCallType = "global::ClosetRpc.Net.IRpcCall";
const std::string kRpcResultType = "global::ClosetRpc.Net.IRpcResult";

inline std::string GetServiceInterfaceName(const std::string &service_name) {
  return service_name + "Interface";
}

inline std::string GetServiceStubBaseName(const std::string &service_name) {
  return service_name + "_StubBase";
}

// csharp_generator.cpp
std::string GetMethodSignature(const code_model::MethodModel &method,
                               const std::string &service_name,
                               bool server);
std::string GetSourcePrologue(const ::google::protobuf::FileDescriptor *file);
std::string GetSourceEpilogue(const ::google::protobuf::FileDescriptor *file);

// csharp_interface_generator.cpp
std::string GetInterfaceDefinitions(
    const ::google::protobuf::FileDescriptor *file,
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
