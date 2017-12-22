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

void GenerateEventMethodCall(pb::io::Printer &printer,
                             const pb::MethodDescriptor &method) {
  std::map<std::string, std::string> vars;

  vars["method_name"] = GetEventMethodName(method.name());

  vars["input_type_name"] = method.input_type()->name();
  bool has_input = !IsVoidType(method.input_type());

  bool has_output = !IsVoidType(method.output_type());

  if (has_input) {
    printer.Print(vars, "var input = new $input_type_name$();\n");
    printer.Print("input.MergeFrom(new CodedInputStream(rpcCall.CallData));\n");
  }

  if (has_output)
    ;  // TODO: Print error (events do not have result)

  if (has_input)
    printer.Print(vars, "this.Impl.$method_name$(input);\n");
  else
    printer.Print(vars, "this.Impl.$method_name$();\n");
}

void GenerateEventMethodCallDispatch(pb::io::Printer &printer,
                                     const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["rpc_status_type"] = kRpcStatusType;

  for (auto i = 0; i < service.method_count(); ++i) {
    const auto &method = *service.method(i);
    vars["method_name"] = method.name();

    printer.Print(vars, "if (rpcCall.MethodName == \"$method_name$\")\n{\n");
    printer.Indent();
    GenerateEventMethodCall(printer, method);
    printer.Outdent();
    printer.Print(vars, "}\n");

    if ((i + 1) < service.method_count())
      printer.Print(vars, "else ");
  }

  // TODO: Generate exception handler
  printer.Print("else\n{\n");
  printer.Indent();
  printer.Print("// TODO: Generate diagnostics?.\n");
  printer.Outdent();
  printer.Print("}\n");
}

void GenerateEventStubBase(pb::io::Printer &printer,
                           const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["service_name"] = service.name();
  vars["interface_name"] = GetEventInterfaceName(service.name());
  vars["service_full_name"] = service.full_name();
  vars["stub_base"] = GetEventStubBaseName(service.name());
  vars["base_class"] = kRpcEventHandlerType;
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

  printer.Print(vars, "protected abstract $interface_name$ Impl { get; }\n\n");

  printer.Print(vars, "public void CallMethod($rpc_call_type$ rpcCall)\n{\n");
  printer.Indent();
  GenerateEventMethodCallDispatch(printer, service);
  printer.Outdent();
  printer.Print(vars, "}\n");
  printer.Outdent();
  printer.Print(vars, "}\n\n");
  // clang-format on
}

void GenerateEventStub(pb::io::Printer &printer,
                       const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["interface_name"] = GetEventInterfaceName(service.name());
  vars["service_full_name"] = service.full_name();
  vars["stub_class_name"] = GetEventStubName(service.name());
  vars["base_class"] = GetEventStubBaseName(service.name());

  // clang-format off
  printer.Print(vars, "public class $stub_class_name$ : $base_class$\n{\n");
  printer.Indent();

  printer.Print(vars, "public $stub_class_name$($interface_name$ impl)\n{\n");
  printer.Indent();
  printer.Print(vars, "this.Impl = impl;\n");
  printer.Outdent();
  printer.Print(vars, "}\n\n");

  printer.Print(vars, "protected override $interface_name$ Impl { get; }\n");

  printer.Outdent();
  printer.Print(vars, "}\n\n");
  // clang-format on
}

void GenerateEventListener(pb::io::Printer &printer,
                           const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["interface_name"] = GetEventInterfaceName(service.name());
  vars["service_full_name"] = service.full_name();
  vars["class_name"] = GetEventListenerName(service.name());
  vars["base_class"] = GetEventStubBaseName(service.name());

  // clang-format off
  printer.Print(vars, "public abstract class $class_name$ : $base_class$, $interface_name$\n{\n");
    printer.Indent();

    printer.Print(vars, "protected override $interface_name$ Impl\n{\n");
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
        GetMethodSignature(method, ContextType::EventStub));
    }

    printer.Outdent();
  printer.Print(vars, "}\n\n");
  // clang-format on
}

void GenerateEventHandlerInvocation(pb::io::Printer &printer,
                                    const pb::MethodDescriptor &method) {
  std::map<std::string, std::string> vars;

  vars["method_name"] = method.name();

  vars["input_type_name"] = method.input_type()->name();
  bool has_input = !IsVoidType(method.input_type());

  printer.Print(vars, "var handler = this.$method_name$;\n");
  printer.Print("if (handler != null)\n{\n");
  printer.Indent();

  if (has_input)
    printer.Print("handler(this, value);\n");
  else
    printer.Print("handler(this);\n");

  printer.Outdent();
  printer.Print(vars, "}\n");
}

void GenerateEventHandlerField(pb::io::Printer &printer,
                               const pb::MethodDescriptor &method) {
  std::map<std::string, std::string> vars;

  vars["method_name"] = method.name();

  vars["input_type_name"] = method.input_type()->name();
  bool has_input = !IsVoidType(method.input_type());

  if (has_input)
    printer.Print(vars, "public event EventHandler<$input_type_name$> $method_name$;\n");
  else
    printer.Print(vars, "public event EventHandler $method_name$;\n");
}

void GenerateEventHandler(pb::io::Printer &printer,
                          const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["interface_name"] = GetEventInterfaceName(service.name());
  vars["service_full_name"] = service.full_name();
  vars["class_name"] = GetEventHandlerName(service.name());
  vars["base_class"] = GetEventStubBaseName(service.name());

  // clang-format off
  printer.Print(vars, "public class $class_name$ : $base_class$, $interface_name$\n{\n");
  printer.Indent();

  for (int i = 0; i < service.method_count(); ++i) {
    const auto &method = *service.method(i);

    if (i > 0)
      printer.Print("\n");

    GenerateEventHandlerField(printer, method);
  }

  printer.Print("\n");
  printer.Print(vars, "protected override $interface_name$ Impl\n{\n");
  printer.Indent();
  printer.Print(vars, "get\n{\n");
  printer.Indent();
  printer.Print(vars, "return this;\n");
  printer.Outdent();
  printer.Print(vars, "}\n");
  printer.Outdent();
  printer.Print(vars, "}\n");

  for (int i = 0; i < service.method_count(); ++i) {
    const auto &method = *service.method(i);

    if (i > 0)
      printer.Print("\n");

    printer.Print("\n");
    printer.Print("public $method_signature$\n", "method_signature",
                  GetMethodSignature(method, ContextType::EventStub));
    printer.Print("{\n");
    printer.Indent();
    GenerateEventHandlerInvocation(printer, method);
    printer.Outdent();
    printer.Print("}\n");
  }

  printer.Outdent();
  printer.Print(vars, "}\n\n");
  // clang-format on
}

void GetEventStubDefinitions(pb::io::Printer &printer,
                             const pb::FileDescriptor &file) {
  std::map<std::string, std::string> vars;

  printer.Indent();
  for (int i = 0; i < file.service_count(); ++i) {
    const auto &service = *file.service(i);
    if (service.options().HasExtension(nanorpc::event_source)) {
      GenerateEventStubBase(printer, service);
      GenerateEventStub(printer, service);
      GenerateEventListener(printer, service);
      GenerateEventHandler(printer, service);
    }
  }
  printer.Outdent();
}

}  // namespace closetrpc_csharp_codegen
