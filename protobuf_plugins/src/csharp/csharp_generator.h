#if !defined(NANORPC_COMPILER_CPP_CSHARP_GENERATOR_H__)
#define NANORPC_COMPILER_CPP_CSHARP_GENERATOR_H__

#include <string>
#include <vector>

namespace google {
namespace protobuf {
class Descriptor;
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

const std::string kRpcNamespace = "global::ClosetRpc";
const std::string kServiceBaseType = "global::ClosetRpc.IRpcService";
const std::string kServerContextType = "global::ClosetRpc.IServerContext";
const std::string kRpcCallType = "global::ClosetRpc.IRpcCall";
const std::string kRpcResultType = "global::ClosetRpc.IRpcResult";
const std::string kRpcClientType = "global::ClosetRpc.Client";
const std::string kRpcStatusType = "global::ClosetRpc.RpcStatus";
const std::string kRpcEventSourceType = "global::ClosetRpc.IEventSource";
const std::string kRpcCallParametersType = "global::ClosetRpc.RpcCallParameters";

enum class MethodSignatureType {
  Stub,
  Proxy,
  EventProxy
};

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
bool IsVoidType(const pb::Descriptor *descriptor);
std::string GetMethodSignature(const pb::MethodDescriptor &method,
                               MethodSignatureType type);
void GetSourcePrologue(pb::io::Printer &printer,
                       const pb::FileDescriptor &file);
void GetSourceEpilogue(pb::io::Printer &printer,
                       const pb::FileDescriptor &file);

// csharp_event_proxy_generator.cpp
void GenerateEventProxy(pb::io::Printer &printer,
                        const pb::ServiceDescriptor &service);

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
