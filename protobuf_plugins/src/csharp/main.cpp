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
#include "google/protobuf/io/zero_copy_stream.h"

#include "common/code_model.h"
#include "common/generator_utils.h"

namespace pb = google::protobuf;
namespace pbc = google::protobuf::compiler;

namespace {
// TODO(jtattermusch): can we reuse a utility function?
std::string UnderscoresToCamelCase(const std::string &input,
                                   bool cap_next_letter,
                                   bool preserve_period) {
  std::string result;
  // Note:  I distrust ctype.h due to locales.
  for (int i = 0; i < input.size(); i++) {
    if ('a' <= input[i] && input[i] <= 'z') {
      if (cap_next_letter) {
        result += input[i] + ('A' - 'a');
      } else {
        result += input[i];
      }
      cap_next_letter = false;
    } else if ('A' <= input[i] && input[i] <= 'Z') {
      if (i == 0 && !cap_next_letter) {
        // Force first letter to lower-case unless explicitly told to
        // capitalize it.
        result += input[i] + ('a' - 'A');
      } else {
        // Capital letters after the first are left as-is.
        result += input[i];
      }
      cap_next_letter = false;
    } else if ('0' <= input[i] && input[i] <= '9') {
      result += input[i];
      cap_next_letter = true;
    } else {
      cap_next_letter = true;
      if (input[i] == '.' && preserve_period) {
        result += '.';
      }
    }
  }
  // Add a trailing "_" if the name should be altered.
  if (input[input.size() - 1] == '#') {
    result += '_';
  }
  return result;
}

std::string UnderscoresToPascalCase(const std::string &input) {
  return UnderscoresToCamelCase(input, true, false);
}

std::string GetFileNamespace(const pb::FileDescriptor *descriptor) {
  if (descriptor->options().has_csharp_namespace()) {
    return descriptor->options().csharp_namespace();
  }
  return UnderscoresToCamelCase(descriptor->package(), true, true);
}
}  // namespace

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
  std::string header_code = GetSourcePrologue(file) +
                            GetInterfaceDefinitions(service_models) +
                            GetStubDefinitions(file, service_models) +
                            //GetProxyDeclarations(service_models) +
                            GetSourceEpilogue(file);
  /* clang-format on */

  // TODO: PB csharp generator is too much of a smart ass trying to create
  // C# like file name with bunch of heuristics. So, deal with it later.
  std::unique_ptr<pb::io::ZeroCopyOutputStream> source_output(
      context->Open(file_name + ".closetrpc.cs"));
  pb::io::CodedOutputStream coded_out(source_output.get());
  coded_out.WriteRaw(header_code.data(), header_code.size());

  ///* clang-format off */
  // std::string source_code = GetSourcePrologue(file) +
  //                           +
  //                          GetProxyDefinitions(service_models) +
  //                          GetSourceEpilogue(file);
  ///* clang-format on */

  // coded_out.WriteRaw(source_code.data(), source_code.size());

  return true;
}

int main(int argc, char **argv) {
  NanoRpcCppGenerator generator;
  return pbc::PluginMain(argc, argv, &generator);
}
