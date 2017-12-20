#include "csharp/csharp_generator.h"

#include <io.h>
#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>

#include <iostream>
#include <fstream>
#include <string>
#include <vector>

#include "google/protobuf/compiler/plugin.h"
#include "google/protobuf/compiler/plugin.pb.h"
#include "google/protobuf/compiler/cpp/cpp_generator.h"
#include "google/protobuf/io/coded_stream.h"
#include "google/protobuf/io/printer.h"
#include "google/protobuf/io/zero_copy_stream.h"

#include "closetrpc_types.pb.h"

#include "common/generator_utils.h"

namespace pb = google::protobuf;
namespace pbc = google::protobuf::compiler;

namespace closetrpc_csharp_codegen {
class NanoRpcCppGenerator : public pbc::CodeGenerator {
public:
  bool Generate(const pb::FileDescriptor *file,
                const std::string &parameter,
                pbc::GeneratorContext *context,
                std::string *error) const override;
};

bool NanoRpcCppGenerator::Generate(const pb::FileDescriptor *file,
                                   const std::string &parameter,
                                   pbc::GeneratorContext *context,
                                   std::string *error) const {
  // __debugbreak();

  std::string file_name = GetFileNamespace(file);

  // TODO: PB csharp generator is too much of a smart ass trying to create
  // C# like file name with bunch of heuristics. So, deal with it later.
  std::unique_ptr<pb::io::ZeroCopyOutputStream> output_stream(
      context->Open(file_name + ".Closetrpc.cs"));
  pb::io::Printer printer(output_stream.get(), '$');

  GetSourcePrologue(printer, *file);

  GetInterfaceDefinitions(printer, *file);
  GetStubDefinitions(printer, *file);
  GetProxyDefinitions(printer, *file);

  //printer.Indent();
  //printer.Print("#region Event classes\n\n");
  //for (int i = 0; i < file->service_count(); ++i) {
  //  const auto &service = *file->service(i);
  //  if (service.options().HasExtension(nanorpc::event_source))
  //    GenerateEventProxy(printer, service);
  //}

  //printer.Print("#endregion\n\n");
  //printer.Outdent();

  GetSourceEpilogue(printer, *file);

  return true;
}

}  // namespace closetrpc_csharp_codegen

int main(int argc, char **argv) {
  closetrpc_csharp_codegen::NanoRpcCppGenerator generator;
  return pbc::PluginMain(argc, argv, &generator);
}
