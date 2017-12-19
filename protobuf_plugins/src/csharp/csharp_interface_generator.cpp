#include "csharp/csharp_generator.h"

#include <string>
#include <vector>

#include "google/protobuf/descriptor.h"
#include "google/protobuf/empty.pb.h"
#include "google/protobuf/service.h"
#include "google/protobuf/io/printer.h"
#include "google/protobuf/io/zero_copy_stream_impl_lite.h"

namespace closetrpc_csharp_codegen {

namespace pb = ::google::protobuf;

void GenerateInterfaceDefinition(pb::io::Printer &printer,
                                 const pb::ServiceDescriptor &service) {
  std::map<std::string, std::string> vars;

  vars["service_interface_name"] = GetInterfaceName(service.name());
  printer.Print(vars, "public interface $service_interface_name$\n{\n");
  printer.Indent();

  for (int i = 0; i < service.method_count(); ++i) {
    const auto &method = *service.method(i);
    vars["method_signature"] = GetMethodSignature(method, true);
    printer.Print(vars, "$method_signature$;\n");
  }

  printer.Outdent();
  printer.Print(vars, "}\n\n");
}

void GetInterfaceDefinitions(pb::io::Printer &printer,
                             const pb::FileDescriptor &file) {
  std::map<std::string, std::string> vars;

  printer.Indent();

  for (int si = 0; si < file.service_count(); ++si) {
    const auto &service = *file.service(si);
    GenerateInterfaceDefinition(printer, service);
  }

  printer.Outdent();
}

}  // namespace closetrpc_csharp_codegen
