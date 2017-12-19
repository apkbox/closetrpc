#include "cpp/cpp_generator.h"

#include <string>
#include <vector>

#include "google/protobuf/io/printer.h"
#include "google/protobuf/io/zero_copy_stream_impl_lite.h"

#include "closetrpc_types.pb.h"

#include "common/generator_utils.h"
#include "common/code_model.h"

namespace pb = google::protobuf;
namespace pbc = google::protobuf::compiler;

std::string GetPropertySignature(const code_model::MethodModel *method,
                                 bool server,
                                 bool setter) {
  std::string output;
  {
    // Scope the output stream so it closes and finalizes output to the string.
    pb::io::StringOutputStream output_stream(&output);
    pb::io::Printer printer(&output_stream, '$');
    std::map<std::string, std::string> vars;

    const code_model::TypeModel &type =
        setter ? method->arguments().front().type() : method->return_type();

    vars["method_name"] = method->name();
    vars["type_name"] = type.name();

    if (type.is_reference_type()) {
      if (setter) {
        printer.Print(vars, "void set_$method_name$(");
        if (server)
          printer.Print(vars, "nanorpc::ServerContextInterface *context, ");
        printer.Print(vars, "const $type_name$ &value)");
      } else {
        printer.Print(vars, "void get_$method_name$(");
        if (server)
          printer.Print(vars, "nanorpc::ServerContextInterface *context, ");
        printer.Print(vars, "$type_name$ *value) const");
      }
    } else {
      if (setter) {
        printer.Print(vars, "void set_$method_name$(");
        if (server)
          printer.Print(vars, "nanorpc::ServerContextInterface *context, ");
        printer.Print(vars, "$type_name$ value)");
      } else {
        printer.Print(vars, "$type_name$ get_$method_name$(");
        if (server)
          printer.Print(vars, "nanorpc::ServerContextInterface *context, ");
        printer.Print(vars, ") const");
      }
    }
  }

  return output;
}

std::string GetMethodSignature(const google::protobuf::MethodDescriptor &method,
                               bool server) {
  std::string output;
  {
    // Scope the output stream so it closes and finalizes output to the string.
    pb::io::StringOutputStream output_stream(&output);
    pb::io::Printer printer(&output_stream, '$');
    std::map<std::string, std::string> vars;

    //vars["service_name_prefix"] = service_name.empty() ? "" : (service_name + "::");
    //vars["method_name"] = method.name();
    //vars["output_type_name"] = method.return_type().name();

    //if (method.return_type().is_reference_type()) {
    //  printer.Print(vars, "void $service_name_prefix$$method_name$(");
    //  if (server)
    //    printer.Print(vars, "nanorpc::ServerContextInterface *context");
    //} else {
    //  printer.Print(vars, "$output_type_name$ $service_name_prefix$$method_name$(");
    //  if (server)
    //    printer.Print(vars, "nanorpc::ServerContextInterface *context");
    //}

    //if (server && (method.arguments().size() > 0 ||
    //               method.return_type().is_reference_type())) {
    //  printer.Print(", ");
    //}

    //for (size_t i = 0; i < method.arguments().size(); ++i) {
    //  const auto &arg = method.arguments()[i];

    //  std::map<std::string, std::string> vars;

    //  vars["arg_type"] = arg.type().name();
    //  vars["const"] = arg.type().is_reference_type() ? "const " : "";
    //  vars["reference"] = arg.type().is_reference_type() ? "&" : "";
    //  vars["arg_name"] = arg.name();
    //  printer.Print(vars, "$const$$arg_type$ $reference$$arg_name$");
    //  if ((i + 1) < method.arguments().size())
    //    printer.Print(", ");
    //}

    //if (method.return_type().is_reference_type()) {
    //  if (method.arguments().size() > 0)
    //    printer.Print(", ");
    //  printer.Print(vars, "$output_type_name$ *out__)");
    //} else {
    //  printer.Print(")");
    //}
  }

  return output;
}

std::string GetHeaderPrologue(const pb::FileDescriptor *file) {
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
    printer.Print(vars, "#ifndef NANORPC_$filename_identifier$__INCLUDED\n");
    printer.Print(vars, "#define NANORPC_$filename_identifier$__INCLUDED\n");
    printer.Print(vars, "\n");
    printer.Print(vars, "#include \"$filename_base$.pb.h\"\n");
    printer.Print(vars, "#include \"nanorpc/nanorpc2.h\"\n");
    printer.Print(vars, "\n");
    /* clang-format on */

    if (!file->package().empty()) {
      std::vector<std::string> parts = tokenize(file->package(), ".");

      for (auto &part = parts.rbegin(); part != parts.rend(); part++) {
        vars["part"] = *part;
        printer.Print(vars, "namespace $part$ {\n");
      }

      printer.Print(vars, "\n");
    }
  }
  return output;
}

std::string GetHeaderEpilogue(const pb::FileDescriptor *file) {
  std::string output;
  {
    // Scope the output stream so it closes and finalizes output to the string.
    pb::io::StringOutputStream output_stream(&output);
    pb::io::Printer printer(&output_stream, '$');
    std::map<std::string, std::string> vars;

    vars["filename"] = file->name();
    vars["filename_identifier"] = FilenameIdentifier(file->name());

    if (!file->package().empty()) {
      std::vector<std::string> parts = tokenize(file->package(), ".");

      for (auto &part = parts.rbegin(); part != parts.rend(); part++) {
        vars["part"] = *part;
        printer.Print(vars, "}  // namespace $part$\n");
      }

      //printer.Print("\nnamespace nanorpc {\n");
      //printer.PrintRaw(GetStubFactoryGetter(file).c_str());
      //printer.Print("}  // namespace nanorpc\n");

      printer.Print(vars, "\n");
    }

    printer.Print(vars, "\n");
    printer.Print(vars, "#endif  // NANORPC_$filename_identifier$__INCLUDED\n");
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
    printer.Print(vars, "#include \"$filename_base$.closetrpc.pb.h\"\n");
    printer.Print(vars, "#include \"google/protobuf/wrappers.pb.h\"\n");
    printer.Print(vars, "\n");
    /* clang-format on */

    if (!file->package().empty()) {
      std::vector<std::string> parts = tokenize(file->package(), ".");

      for (auto &part = parts.rbegin(); part != parts.rend(); part++) {
        vars["part"] = *part;
        printer.Print(vars, "namespace $part$ {\n");
      }

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
      std::vector<std::string> parts = tokenize(file->package(), ".");

      for (auto &part = parts.rbegin(); part != parts.rend(); part++) {
        vars["part"] = *part;
        printer.Print(vars, "}  // namespace $part$\n");
      }

      printer.Print(vars, "\n");
    }

    printer.Print(vars, "\n");
  }
  return output;
}