#include "csharp/csharp_generator.h"

#include <string>
#include <vector>

#include "google\protobuf\io\printer.h"
#include "google\protobuf\io\zero_copy_stream_impl_lite.h"

#include "common/code_model.h"

namespace pb = ::google::protobuf;

std::string GetInterfaceDefinitions(
    const google::protobuf::FileDescriptor *file,
    const std::vector<code_model::ServiceModel> &models) {
  std::string output;
  {
  // Scope the output stream so it closes and finalizes output to the string.
  pb::io::StringOutputStream output_stream(&output);
  pb::io::Printer printer(&output_stream, '$');
  std::map<std::string, std::string> vars;

  printer.Indent();

  for (const auto &service_model : models) {
    vars["service_name"] = service_model.name();
    printer.Print(vars, "public interface $service_name$Interface {\n");
    printer.Indent();

    for (const auto &method_model : service_model.methods()) {
      vars["method_name"] = method_model.name();
      vars["method_signature"] =
          GetMethodSignature(method_model, std::string(), true);
      printer.Print(vars, "$method_signature$;\n");
    }

    printer.Outdent();
    printer.Print(vars, "}\n\n");
  }

    printer.Outdent();
  }
  return output;
}
