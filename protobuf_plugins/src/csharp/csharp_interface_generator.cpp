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

void GenerateInterfaceDefinition(pb::io::Printer &printer,
                                 const pb::ServiceDescriptor &service,
                                 bool event_interface) {
  std::map<std::string, std::string> vars;

  vars["service_interface_name"] = event_interface
                                       ? GetEventInterfaceName(service.name())
                                       : GetInterfaceName(service.name());
  printer.Print(vars, "public interface $service_interface_name$\n{\n");
  printer.Indent();

  for (int i = 0; i < service.method_count(); ++i) {
    const auto &method = *service.method(i);
    if (i > 0)
      printer.Print("\n");

    auto context = event_interface ? ContextType::EventStub : ContextType::Stub;
    auto signature = GetMethodSignature(method, context);
    printer.Print("$method_signature$;\n", "method_signature", signature);
  }

  printer.Outdent();
  printer.Print(vars, "}\n\n");
}

void GetInterfaceDefinitions(pb::io::Printer &printer,
                             const pb::FileDescriptor &file) {
  std::map<std::string, std::string> vars;

  for (int si = 0; si < file.service_count(); ++si) {
    const auto &service = *file.service(si);
    if (!service.options().HasExtension(nanorpc::event_source))
      GenerateInterfaceDefinition(printer, service, false);
  }
}

}  // namespace closetrpc_csharp_codegen
