#include "csharp/csharp_generator.h"

#include <string>
#include <vector>

#include "google\protobuf\io\printer.h"
#include "google\protobuf\io\zero_copy_stream_impl_lite.h"

#include "common/code_model.h"
#include "common/generator_utils.h"

namespace pb = ::google::protobuf;

void GenerateStubMethodCallImplementation(
    pb::io::Printer &printer,
    const code_model::MethodModel &method) {
  std::map<std::string, std::string> vars;

  vars["method_name"] = method.name();
  vars["return_type"] = method.return_type().name();
  vars["return_wrapper_name"] = method.return_type().wrapper_name();

  if (method.arguments().size() == 1) {
    const auto &arg = method.arguments()[0];

    // Declare wrapper variable for single argument calls and arglist calls.
    vars["arg_type"] = arg.type().wrapper_name();
    vars["arg_name"] = arg.name();
    printer.Print(vars, "$arg_type$ in_arg__;\n\n");

    vars["arg_type"] = arg.type().name();
    vars["const"] = arg.type().is_reference_type() ? "const " : "";
    vars["ref"] = arg.type().is_reference_type() ? "&" : "";
    printer.Print(vars, "in_arg__.ParseFromString(rpc_call.call_data());\n");
    if (arg.type().is_struct())
      printer.Print(vars, "$const$$arg_type$ &$arg_name$ = in_arg__;\n");
    else
      printer.Print(vars, "$const$$arg_type$ $ref$value = in_arg__.value();\n");
  }

  if (method.arguments().size() > 0)
    printer.Print(vars, "\n");

  // Define return type variable
  if (!method.return_type().is_void()) {
    printer.Print(vars, "$return_type$ out__;\n");
    if (!method.return_type().is_struct())
      printer.Print(vars, "$return_wrapper_name$ out_pb__;\n");
  }

  // Return for value types
  if (!method.return_type().is_void() &&
      !method.return_type().is_reference_type())
    printer.Print(vars, "out__ = ");

  // Call interface method
  printer.Print(vars, "impl_->$method_name$(nullptr");
  if (method.arguments().size() > 0 ||
      (!method.return_type().is_void() &&
       method.return_type().is_reference_type())) {
    printer.Print(", ");
  }

  // Specify arguments
  for (size_t j = 0; j < method.arguments().size(); ++j) {
    const auto &arg = method.arguments()[j];
    vars["arg_name"] = arg.name();
    vars["arg_type"] = arg.type().name();
    printer.Print(vars, "$arg_name$");

    if ((j + 1) < method.arguments().size())
      printer.Print(vars, ", ");
  }

  // Specify return by pointer argument (for reference types)
  if (!method.return_type().is_void() &&
      method.return_type().is_reference_type()) {
    if (method.arguments().size() > 0)
      printer.Print(vars, ", ");
    printer.Print(vars, "&out__");
  }

  printer.Print(vars, ");\n");

  // Define result wrapper variable, wrap and serialize the result
  if (!method.return_type().is_void()) {
    printer.Print(vars, "\n");
    if (method.return_type().is_struct()) {
      /* clang-format off */
      printer.Print(vars, "out__.SerializeToString(rpc_result->mutable_result_data());\n");
      /* clang-format on */
    } else {
      /* clang-format off */
      printer.Print(vars, "out_pb__.set_value(out__);\n");
      printer.Print(vars, "out_pb__.SerializeToString(rpc_result->mutable_result_data());\n");
      /* clang-format on */
    }
  }

  if (method.is_async())
    printer.Print(vars, "return false;\n");
  else
    printer.Print(vars, "return true;\n");
}

void GenerateStubImplementation(pb::io::Printer &printer,
                                const code_model::ServiceModel &service) {
  std::map<std::string, std::string> vars;

  printer.Indent();

  for (size_t i = 0; i < service.methods().size(); ++i) {
    const auto &method = service.methods()[i];
    vars["method_name"] = method.name();

    printer.Print(vars, "if (rpcCall.MethodName == \"$method_name$\") {\n");
    printer.Indent();
    GenerateStubMethodCallImplementation(printer, method);
    printer.Outdent();
    printer.Print(vars, "}");

    if ((i + 1) < service.methods().size())
      printer.Print(vars, " else ");
  }

  printer.Print(vars, "\n\n");

  // TODO: Generate unknown method error into result
  // TODO: Generate exception handler
  printer.Print(
      vars,
      "// TODO: Here should be unknown method error stored into rpc_result.\n");
  printer.Print(
      vars,
      "// TODO: Also an exception (code above must be guarded) result.\n\n");
  printer.Outdent();
}

std::string GetStubBaseDefinitions(
  const pb::FileDescriptor *file,
  const std::vector<code_model::ServiceModel> &models) {
  std::string output;
  {
    // Scope the output stream so it closes and finalizes output to the string.
    pb::io::StringOutputStream output_stream(&output);
    pb::io::Printer printer(&output_stream, '$');
    std::map<std::string, std::string> vars;

    printer.Indent();

    for (const auto &service : models) {
      vars["service_name"] = service.name();
      vars["service_interface_name"] = GetServiceInterfaceName(service.name());
      vars["service_full_name"] = service.full_name();
      vars["stub_base"] = GetServiceStubBaseName(service.name());
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

      printer.Print(vars, "public void CallMethod($server_context_type$ context, $rpc_call_type$ rpcCall, $rpc_result_type$ rpcResult) {\n");
      printer.Indent();
      GenerateStubImplementation(printer, service);
      printer.Outdent();
      printer.Print(vars, "}\n\n");
      // clang-format on
    }
  }
  return output;
}

std::string GetStubDefinitions(
    const pb::FileDescriptor *file,
    const std::vector<code_model::ServiceModel> &models) {
  std::string output;

  //output = GetStubBaseDefinitions(file, models);

  {
    // Scope the output stream so it closes and finalizes output to the string.
    pb::io::StringOutputStream output_stream(&output);
    pb::io::Printer printer(&output_stream, '$');
    std::map<std::string, std::string> vars;

    printer.Indent();

    for (const auto &service : models) {
      vars["service_name"] = service.name();
      vars["service_full_name"] = service.full_name();

      // clang-format off
      printer.Print(vars, "public class $service_name$_Stub : IRpcService\n{\n");
      printer.Indent();
      printer.Print(vars, "$service_name$_Stub($service_name$ impl)\n{\n");
      printer.Indent();
      printer.Print(vars, "impl_ = impl;\n");
      printer.Outdent();
      printer.Print(vars, "}\n\n");
      printer.Print(vars, "public string Name\n{\n");
      printer.Indent();
      printer.Print(vars, "get\n{\n");
      printer.Indent();
      printer.Print(vars, "return \"$service_full_name$\";\n");
      printer.Outdent();
      printer.Print(vars, "}\n");
      printer.Outdent();
      printer.Print(vars, "}\n\n");
      printer.Print("// TODO: Context in event stub is meaningless, so at some point we should\n");
      printer.Print("// differentiate server and event stub signatures.\n");
      printer.Print(vars, "public void CallMethod(ServerContext context, IRpcCall rpcCall, IRpcResult rpcResult) {\n");
      printer.Indent();
      GenerateStubImplementation(printer, service);
      printer.Outdent();
      printer.Print(vars, "}\n\n");
      printer.Print(vars, "private $service_name$ impl_;\n");
      printer.Outdent();
      printer.Print(vars, "}\n\n");
      // clang-format on
    }

    printer.Outdent();
  }
  return output;
}

