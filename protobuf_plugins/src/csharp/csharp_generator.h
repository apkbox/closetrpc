#if !defined(NANORPC_COMPILER_CPP_CSHARP_GENERATOR_H__)
#define NANORPC_COMPILER_CPP_CSHARP_GENERATOR_H__

#include <string>
#include <vector>

namespace google {
namespace protobuf {
class FileDescriptor;
class ServiceDescriptor;
class MethodDescriptor;
namespace io {
class Printer;
}  // namespace io
}  // namespace protobuf
}  // namespace google

namespace closetrpc_csharp_codegen {

namespace pb = ::google::protobuf;

const std::string kRpcNamespace = "global::ClosetRpc.Net";
const std::string kServiceBaseType = "global::ClosetRpc.Net.IRpcService";
const std::string kServerContextType = "global::ClosetRpc.Net.ServerContext";
const std::string kRpcCallType = "global::ClosetRpc.Net.IRpcCall";
const std::string kRpcResultType = "global::ClosetRpc.Net.IRpcResult";
const std::string kRpcClientType = "global::ClosetRpc.Net.Client";
const std::string kRpcStatusType = "global::ClosetRpc.Net.RpcStatus";

inline std::string GetInterfaceName(const std::string &service_name) {
  return service_name + "Interface";
}

inline std::string GetStubBaseName(const std::string &service_name) {
  return service_name + "_StubBase";
}

inline std::string GetStubName(const std::string &service_name) {
  return service_name + "_Stub";
}

inline std::string GetServiceBaseName(const std::string &service_name) {
  return service_name + "_ServiceBase";
}

inline std::string GetProxyName(const std::string &service_name) {
  return service_name + "_Proxy";
}

// csharp_generator.cpp
std::string GetFileNamespace(const pb::FileDescriptor *descriptor);

std::string GetMethodSignature(const pb::MethodDescriptor &method, bool server);
void GetSourcePrologue(pb::io::Printer &printer,
                       const pb::FileDescriptor &file);
void GetSourceEpilogue(pb::io::Printer &printer,
                       const pb::FileDescriptor &file);

// csharp_interface_generator.cpp
void GenerateInterfaceDefinition(pb::io::Printer &printer,
                                 const pb::ServiceDescriptor &service);

void GetInterfaceDefinitions(pb::io::Printer &printer,
                             const pb::FileDescriptor &file);

// csharp_proxy_generator.cpp
void GenerateProxy(pb::io::Printer &printer,
                   const pb::ServiceDescriptor &service);
void GetProxyDefinitions(pb::io::Printer &printer,
                         const pb::FileDescriptor &file);

// csharp_stub_generator.cpp
void GenerateStubBase(pb::io::Printer &printer,
                      const pb::ServiceDescriptor &service);
void GenerateStub(pb::io::Printer &printer,
                  const pb::ServiceDescriptor &service);
void GenerateServiceBase(pb::io::Printer &printer,
                         const pb::ServiceDescriptor &service);

void GetStubDefinitions(pb::io::Printer &printer,
                        const pb::FileDescriptor &file);

}  // namespace closetrpc_csharp_codegen

#endif  // NANORPC_COMPILER_CPP_CSHARP_GENERATOR_H__
