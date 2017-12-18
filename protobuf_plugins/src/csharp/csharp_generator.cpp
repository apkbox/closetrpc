#include "csharp/csharp_generator.h"

#include <string>
#include <vector>

#include "google/protobuf/io/printer.h"
#include "google/protobuf/io/zero_copy_stream_impl_lite.h"

#include "closetrpc_types.pb.h"

#include "common/generator_utils.h"
#include "common/code_model.h"

namespace pb = google::protobuf;
namespace pbc = google::protobuf::compiler;

std::string GetMethodSignature(const code_model::MethodModel &method,
                               const std::string &service_name,
                               bool server) {
  std::string output;
  {
    // Scope the output stream so it closes and finalizes output to the string.
    pb::io::StringOutputStream output_stream(&output);
    pb::io::Printer printer(&output_stream, '$');
    std::map<std::string, std::string> vars;

    vars["rpc_ns"] = kRpcNamespace;
    vars["service_name_prefix"] = service_name.empty() ? "" : (service_name + "::");
    vars["method_name"] = method.name();
    vars["output_type_name"] = method.return_type().csharp_name();

    printer.Print(vars, "$output_type_name$ $service_name_prefix$$method_name$(");
    if (server)
      printer.Print(vars, "$rpc_ns$.IServerContext context");

    if (server && (method.arguments().size() > 0)) {
      printer.Print(", ");
    }

    for (size_t i = 0; i < method.arguments().size(); ++i) {
      const auto &arg = method.arguments()[i];

      std::map<std::string, std::string> vars;

      vars["arg_type"] = arg.type().csharp_name();
      vars["arg_name"] = arg.name();
      printer.Print(vars, "$arg_type$ $arg_name$");
      if ((i + 1) < method.arguments().size())
        printer.Print(", ");
    }

    printer.Print(")");
  }

  return output;
}

std::string GetSourcePrologue(const pb::FileDescriptor *file) {
  std::string output;
  {
    // Scope the output stream so it closes and finalizes output to the string.
    pb::io::StringOutputStream output_stream(&output);
    pb::io::Printer printer(&output_stream, '$');
    std::map<std::string, std::string> vars;

    vars["filename"] = file->name();
    vars["filename_identifier"] = FilenameIdentifier(file->name());
    vars["filename_base"] = StripProto(file->name());

    /* clang-format off */
    printer.Print(vars, "// Generated by the nanorpc protobuf plugin.\n");
    printer.Print(vars, "// If you make any local change, they will be lost.\n");
    printer.Print(vars, "// source: $filename$\n");
    printer.Print(vars, "\n");
    printer.Print(vars, "\n");
    /* clang-format on */

    if (!file->package().empty()) {
      vars["ns"] = file->package();
      printer.Print(vars, "namespace $ns$ {\n");
      printer.Print(vars, "\n");
    }
  }
  return output;
}

std::string GetSourceEpilogue(const pb::FileDescriptor *file) {
  std::string output;
  {
    // Scope the output stream so it closes and finalizes output to the string.
    pb::io::StringOutputStream output_stream(&output);
    pb::io::Printer printer(&output_stream, '$');
    std::map<std::string, std::string> vars;

    vars["filename"] = file->name();
    vars["filename_identifier"] = FilenameIdentifier(file->name());

    if (!file->package().empty()) {
      vars["ns"] = file->package();
      printer.Print(vars, "}  // namespace $ns$\n");
      printer.Print(vars, "\n");
    }

    printer.Print(vars, "\n");
  }
  return output;
}
