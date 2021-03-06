#include "cpp/cpp_generator.h"

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
#include "google/protobuf/io/zero_copy_stream.h"

#include "common/code_model.h"
#include "common/generator_utils.h"

namespace pb = google::protobuf;
namespace pbc = google::protobuf::compiler;

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
  std::vector<code_model::ServiceModel> service_models;
  // __debugbreak();
  if (!code_model::CreateCodeModel(file, &service_models)) {
    return false;
  }

  std::string file_name = StripProto(file->name());

  /* clang-format off */
  std::string header_code = GetHeaderPrologue(file) +
                            GetInterfaceDefinitions(file, service_models) +
                            GetStubDeclarations(file) +
                            GetProxyDeclarations(service_models) +
                            GetHeaderEpilogue(file);
  /* clang-format on */

  std::unique_ptr<pb::io::ZeroCopyOutputStream> header_output(
      context->Open(file_name + ".closetrpc.pb.h"));
  pb::io::CodedOutputStream header_coded_out(header_output.get());
  header_coded_out.WriteRaw(header_code.data(), header_code.size());

  /* clang-format off */
  std::string source_code = GetSourcePrologue(file) +
                            GetStubDefinitions(file, service_models) +
                            GetProxyDefinitions(service_models) +
                            GetSourceEpilogue(file);
  /* clang-format on */

  std::unique_ptr<pb::io::ZeroCopyOutputStream> source_output(
      context->Open(file_name + ".closetrpc.pb.cc"));
  pb::io::CodedOutputStream source_coded_out(source_output.get());
  source_coded_out.WriteRaw(source_code.data(), source_code.size());

  return true;
}

int main(int argc, char **argv) {
  NanoRpcCppGenerator generator;
  return pbc::PluginMain(argc, argv, &generator);
}
