#include "csharp/csharp_generator.h"

#include <string>
#include <vector>

#include "google/protobuf/descriptor.h"
#include "google/protobuf/empty.pb.h"
#include "google/protobuf/service.h"
#include "google/protobuf/io/printer.h"
#include "google/protobuf/io/zero_copy_stream_impl_lite.h"

#include "closetrpc_types.pb.h"

namespace closetrpc_csharp_codegen {

namespace pb = ::google::protobuf;

void GenerateProxyMethods(pb::io::Printer &printer,
                          const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["rpc_status_type"] = kRpcStatusType;

  for (int i = 0; i < service.method_count(); ++i) {
    const auto &method = *service.method(i);
    vars["method_signature"] = GetMethodSignature(method, false);
    printer.Print(vars, "public $method_signature$\n{\n");
    printer.Indent();
    vars["method_name"] = method.name();
    vars["proxy_name"] = GetProxyName(method.service()->name());
    vars["service_name"] = method.service()->name();

    vars["input_type_name"] = method.input_type()->name();
    bool has_input = method.input_type()->full_name() !=
                     pb::Empty::descriptor()->full_name();

    vars["output_type_name"] = method.output_type()->name();
    bool has_output = method.output_type()->full_name() !=
                      pb::Empty::descriptor()->full_name();

    printer.Print(vars, "var call = this.client.CreateCallBuilder();\n");
    printer.Print(vars, "call.ServiceName = $proxy_name$.ServiceName;\n");
    printer.Print(vars, "call.MethodName = \"$method_name$\";\n");

    if (method.options().HasExtension(nanorpc::async))
      printer.Print(vars, "call.IsAsync = true;\n");

    if (has_input)
      printer.Print(vars, "call.CallData = value.ToByteArray();\n");

    printer.Print(vars, "var result = this.client.CallService(call);\n");
    printer.Print(vars, "if (result.Status != $rpc_status_type$.Succeeded)\n{\n");
    printer.Indent();
    printer.Print(vars, "throw new Exception(); // TODO: Be more specific\n");
    printer.Outdent();
    printer.Print(vars, "}\n\n");

    if (has_output) {
      printer.Print(vars, "var returnValue = new $output_type_name$();\n");
      printer.Print(vars, "returnValue.MergeFrom(result.ResultData);\n");
      printer.Print(vars, "return returnValue;\n");
    }
    printer.Outdent();
    printer.Print(vars, "}\n\n");
  }
}

void GenerateProxy(pb::io::Printer &printer,
                   const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["proxy_name"] = GetProxyName(service.name());
  vars["service_name"] = service.name();
  vars["service_interface_name"] = GetInterfaceName(service.name());
  vars["service_full_name"] = service.full_name();
  vars["server_context_type"] = kServerContextType;
  vars["rpc_call_type"] = kRpcCallType;
  vars["rpc_result_type"] = kRpcResultType;
  vars["rpc_client_type"] = kRpcClientType;

  // clang-format off
  printer.Print(vars, "public class $proxy_name$\n{\n");
  printer.Indent();
  printer.Print(vars, "private static readonly string ServiceName = \"$service_full_name$\";\n\n");
  printer.Print(vars, "private readonly $rpc_client_type$ client;\n\n");
  printer.Print(vars, "public $proxy_name$($rpc_client_type$ client)\n{\n");
  printer.Indent();
  printer.Print(vars, "this.client = client;\n");
  printer.Outdent();
  printer.Print(vars, "}\n\n");

  GenerateProxyMethods(printer, service);

  printer.Outdent();
  printer.Print(vars, "}\n\n");
  // clang-format on
}

void GetProxyDefinitions(pb::io::Printer &printer,
                         const pb::FileDescriptor &file) {
  std::map<std::string, std::string> vars;
  printer.Indent();
  for (int i = 0; i < file.service_count(); ++i) {
    const auto &service = *file.service(i);
    GenerateProxy(printer, service);
  }
  printer.Outdent();
}

}  // namespace closetrpc_csharp_codegen
