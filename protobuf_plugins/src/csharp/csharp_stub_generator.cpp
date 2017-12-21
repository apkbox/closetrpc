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

void GenerateServiceMethodCall(pb::io::Printer &printer,
                               const pb::MethodDescriptor &method) {
  std::map<std::string, std::string> vars;

  vars["method_name"] = method.name();

  vars["input_type_name"] = method.input_type()->name();
  bool has_input = !IsVoidType(method.input_type());

  vars["output_type_name"] = method.output_type()->name();
  bool has_output = !IsVoidType(method.output_type());

  if (has_input) {
    printer.Print(vars, "var input = new $input_type_name$();\n");
    printer.Print("input.MergeFrom(new CodedInputStream(rpcCall.CallData));\n");
  }

  if (has_output)
    printer.Print(vars, "var result = ");

  if (has_input)
    printer.Print(vars, "this.Impl.$method_name$(context, input);\n");
  else
    printer.Print(vars, "this.Impl.$method_name$(context);\n");

  if (has_output) {
    printer.Print(vars, "rpcResult.ResultData = result.ToByteArray();\n");
  }
}

void GenerateServiceMethodCallDispatch(pb::io::Printer &printer,
                                       const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["rpc_status_type"] = kRpcStatusType;

  printer.Print(vars, "rpcResult.Status = $rpc_status_type$.Succeeded;\n\n");

  for (auto i = 0; i < service.method_count(); ++i) {
    const auto &method = *service.method(i);
    vars["method_name"] = method.name();

    printer.Print(vars, "if (rpcCall.MethodName == \"$method_name$\")\n{\n");
    printer.Indent();
    GenerateServiceMethodCall(printer, method);
    printer.Outdent();
    printer.Print(vars, "}\n");

    if ((i + 1) < service.method_count())
      printer.Print(vars, "else ");
  }

  // TODO: Generate exception handler
  printer.Print("else\n{\n");
  printer.Indent();
  printer.Print(vars, "rpcResult.Status = $rpc_status_type$.UnknownMethod;\n");
  printer.Outdent();
  printer.Print("}\n");
  printer.Print("\n// TODO: Generate an exception handling code.\n");
}

void GenerateStubBase(pb::io::Printer &printer,
                      const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["service_name"] = service.name();
  vars["service_interface_name"] = GetInterfaceName(service.name());
  vars["service_full_name"] = service.full_name();
  vars["stub_base"] = GetStubBaseName(service.name());
  vars["base_class"] = kServiceBaseType;
  vars["server_context_type"] = kServerContextType;
  vars["rpc_call_type"] = kRpcCallType;
  vars["rpc_result_type"] = kRpcResultType;

  // clang-format off
  printer.Print(vars, "public abstract class $stub_base$ : $base_class$\n{\n");
  printer.Indent();
  printer.Print(vars, "public string Name\n{\n");
  printer.Indent();
  printer.Print(vars, "get\n{\n");
  printer.Indent();
  printer.Print(vars, "return \"$service_full_name$\";\n");
  printer.Outdent();
  printer.Print(vars, "}\n");
  printer.Outdent();
  printer.Print(vars, "}\n\n");

  printer.Print(vars, "protected abstract $service_interface_name$ Impl { get; }\n\n");

  printer.Print(vars, "public void CallMethod($server_context_type$ context, $rpc_call_type$ rpcCall, $rpc_result_type$ rpcResult)\n{\n");
  printer.Indent();
  GenerateServiceMethodCallDispatch(printer, service);
  printer.Outdent();
  printer.Print(vars, "}\n");
  printer.Outdent();
  printer.Print(vars, "}\n\n");
  // clang-format on
}

void GenerateStub(pb::io::Printer &printer,
                  const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["service_interface_name"] = GetInterfaceName(service.name());
  vars["service_full_name"] = service.full_name();
  vars["stub_class_name"] = GetStubName(service.name());
  vars["base_class"] = GetStubBaseName(service.name());

  // clang-format off
  printer.Print(vars, "public class $stub_class_name$ : $base_class$\n{\n");
  printer.Indent();
  printer.Print(vars, "public $stub_class_name$($service_interface_name$ impl)\n{\n");
  printer.Indent();
  printer.Print(vars, "this.Impl = impl;\n");
  printer.Outdent();
  printer.Print(vars, "}\n\n");
  printer.Print(vars, "protected override $service_interface_name$ Impl { get; }\n");
  printer.Outdent();
  printer.Print(vars, "}\n\n");
  // clang-format on
}

void GenerateServiceBase(pb::io::Printer &printer,
                         const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["service_name"] = service.name();
  vars["service_interface_name"] = GetInterfaceName(service.name());
  vars["service_full_name"] = service.full_name();
  vars["class_name"] = GetServiceBaseName(service.name());
  vars["stub_base"] = GetStubBaseName(service.name());
  vars["base_class_name"] = kServiceBaseType;
  vars["server_context_type"] = kServerContextType;
  vars["rpc_call_type"] = kRpcCallType;
  vars["rpc_result_type"] = kRpcResultType;

  // clang-format off
  printer.Print(vars, "public abstract class $class_name$ : $stub_base$, $service_interface_name$\n{\n");
  printer.Indent();
  printer.Print(vars, "protected override $service_interface_name$ Impl\n{\n");
  printer.Indent();
  printer.Print(vars, "get\n{\n");
  printer.Indent();
  printer.Print(vars, "return this;\n");
  printer.Outdent();
  printer.Print(vars, "}\n");
  printer.Outdent();
  printer.Print(vars, "}\n\n");

  for (int i = 0; i < service.method_count(); ++i) {
    const auto &method = *service.method(i);

    if (i > 0)
      printer.Print("\n");

    printer.Print("public abstract $method_signature$;\n", "method_signature",
                  GetMethodSignature(method, MethodSignatureType::Stub));
  }

  printer.Outdent();
  printer.Print(vars, "}\n\n");
  // clang-format on

}

void GetStubDefinitions(pb::io::Printer &printer,
                        const pb::FileDescriptor &file) {
  std::map<std::string, std::string> vars;

  printer.Indent();
  for (int i = 0; i < file.service_count(); ++i) {
    const auto &service = *file.service(i);
    if (!service.options().HasExtension(nanorpc::event_source)) {
      GenerateStubBase(printer, service);
      GenerateStub(printer, service);
      GenerateServiceBase(printer, service);
    }
  }
  printer.Outdent();
}

}  // namespace closetrpc_csharp_codegen
