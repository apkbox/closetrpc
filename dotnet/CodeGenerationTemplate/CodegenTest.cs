// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: codegen_test.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace CodegenTest {

  /// <summary>Holder for reflection information generated from codegen_test.proto</summary>
  public static partial class CodegenTestReflection {

    #region Descriptor
    /// <summary>File descriptor for codegen_test.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CodegenTestReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChJjb2RlZ2VuX3Rlc3QucHJvdG8SDGNvZGVnZW5fdGVzdBobZ29vZ2xlL3By",
            "b3RvYnVmL2VtcHR5LnByb3RvGhVjbG9zZXRycGNfdHlwZXMucHJvdG8ijwIK",
            "ClN0cnVjdFR5cGUSEgoKYm9vbF92YWx1ZRgBIAEoCBITCgtpbnQzMl92YWx1",
            "ZRgCIAEoBRIUCgx1aW50MzJfdmFsdWUYAyABKA0SEwoLaW50NjRfdmFsdWUY",
            "BCABKAMSFAoMdWludDY0X3ZhbHVlGAUgASgEEhQKDHNpbnQzMl92YWx1ZRgG",
            "IAEoERIUCgxzaW50NjRfdmFsdWUYByABKBISEwoLZmxvYXRfdmFsdWUYCCAB",
            "KAISFAoMZG91YmxlX3ZhbHVlGAkgASgBEioKCmVudW1fdmFsdWUYCiABKA4y",
            "Fi5jb2RlZ2VuX3Rlc3QuRW51bVR5cGUSFAoMc3RyaW5nX3ZhbHVlGAsgASgJ",
            "KloKCEVudW1UeXBlEg4KCkVudW1WYWx1ZTAQABIOCgpFbnVtVmFsdWUxEAES",
            "DgoKRW51bVZhbHVlMhACEg4KCkVudW1WYWx1ZTMQAxIOCgpFbnVtVmFsdWU0",
            "EAQyoQMKC1Rlc3RTZXJ2aWNlEjwKCk1ldGhvZF9WX1YSFi5nb29nbGUucHJv",
            "dG9idWYuRW1wdHkaFi5nb29nbGUucHJvdG9idWYuRW1wdHkSPgoKTWV0aG9k",
            "X1ZfTRIYLmNvZGVnZW5fdGVzdC5TdHJ1Y3RUeXBlGhYuZ29vZ2xlLnByb3Rv",
            "YnVmLkVtcHR5Ej4KCk1ldGhvZF9NX1YSFi5nb29nbGUucHJvdG9idWYuRW1w",
            "dHkaGC5jb2RlZ2VuX3Rlc3QuU3RydWN0VHlwZRJACgpNZXRob2RfTV9NEhgu",
            "Y29kZWdlbl90ZXN0LlN0cnVjdFR5cGUaGC5jb2RlZ2VuX3Rlc3QuU3RydWN0",
            "VHlwZRJHCg9Bc3luY01ldGhvZF9WX1YSFi5nb29nbGUucHJvdG9idWYuRW1w",
            "dHkaFi5nb29nbGUucHJvdG9idWYuRW1wdHkiBJCSLAESSQoPQXN5bmNNZXRo",
            "b2RfVl9NEhguY29kZWdlbl90ZXN0LlN0cnVjdFR5cGUaFi5nb29nbGUucHJv",
            "dG9idWYuRW1wdHkiBJCSLAFiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.EmptyReflection.Descriptor, global::ClosetRpc.ClosetrpcTypesReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::CodegenTest.EnumType), }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::CodegenTest.StructType), global::CodegenTest.StructType.Parser, new[]{ "BoolValue", "Int32Value", "Uint32Value", "Int64Value", "Uint64Value", "Sint32Value", "Sint64Value", "FloatValue", "DoubleValue", "EnumValue", "StringValue" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum EnumType {
    [pbr::OriginalName("EnumValue0")] EnumValue0 = 0,
    [pbr::OriginalName("EnumValue1")] EnumValue1 = 1,
    [pbr::OriginalName("EnumValue2")] EnumValue2 = 2,
    [pbr::OriginalName("EnumValue3")] EnumValue3 = 3,
    [pbr::OriginalName("EnumValue4")] EnumValue4 = 4,
  }

  #endregion

  #region Messages
  public sealed partial class StructType : pb::IMessage<StructType> {
    private static readonly pb::MessageParser<StructType> _parser = new pb::MessageParser<StructType>(() => new StructType());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<StructType> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CodegenTest.CodegenTestReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public StructType() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public StructType(StructType other) : this() {
      boolValue_ = other.boolValue_;
      int32Value_ = other.int32Value_;
      uint32Value_ = other.uint32Value_;
      int64Value_ = other.int64Value_;
      uint64Value_ = other.uint64Value_;
      sint32Value_ = other.sint32Value_;
      sint64Value_ = other.sint64Value_;
      floatValue_ = other.floatValue_;
      doubleValue_ = other.doubleValue_;
      enumValue_ = other.enumValue_;
      stringValue_ = other.stringValue_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public StructType Clone() {
      return new StructType(this);
    }

    /// <summary>Field number for the "bool_value" field.</summary>
    public const int BoolValueFieldNumber = 1;
    private bool boolValue_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool BoolValue {
      get { return boolValue_; }
      set {
        boolValue_ = value;
      }
    }

    /// <summary>Field number for the "int32_value" field.</summary>
    public const int Int32ValueFieldNumber = 2;
    private int int32Value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Int32Value {
      get { return int32Value_; }
      set {
        int32Value_ = value;
      }
    }

    /// <summary>Field number for the "uint32_value" field.</summary>
    public const int Uint32ValueFieldNumber = 3;
    private uint uint32Value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint Uint32Value {
      get { return uint32Value_; }
      set {
        uint32Value_ = value;
      }
    }

    /// <summary>Field number for the "int64_value" field.</summary>
    public const int Int64ValueFieldNumber = 4;
    private long int64Value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Int64Value {
      get { return int64Value_; }
      set {
        int64Value_ = value;
      }
    }

    /// <summary>Field number for the "uint64_value" field.</summary>
    public const int Uint64ValueFieldNumber = 5;
    private ulong uint64Value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong Uint64Value {
      get { return uint64Value_; }
      set {
        uint64Value_ = value;
      }
    }

    /// <summary>Field number for the "sint32_value" field.</summary>
    public const int Sint32ValueFieldNumber = 6;
    private int sint32Value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Sint32Value {
      get { return sint32Value_; }
      set {
        sint32Value_ = value;
      }
    }

    /// <summary>Field number for the "sint64_value" field.</summary>
    public const int Sint64ValueFieldNumber = 7;
    private long sint64Value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Sint64Value {
      get { return sint64Value_; }
      set {
        sint64Value_ = value;
      }
    }

    /// <summary>Field number for the "float_value" field.</summary>
    public const int FloatValueFieldNumber = 8;
    private float floatValue_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float FloatValue {
      get { return floatValue_; }
      set {
        floatValue_ = value;
      }
    }

    /// <summary>Field number for the "double_value" field.</summary>
    public const int DoubleValueFieldNumber = 9;
    private double doubleValue_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public double DoubleValue {
      get { return doubleValue_; }
      set {
        doubleValue_ = value;
      }
    }

    /// <summary>Field number for the "enum_value" field.</summary>
    public const int EnumValueFieldNumber = 10;
    private global::CodegenTest.EnumType enumValue_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::CodegenTest.EnumType EnumValue {
      get { return enumValue_; }
      set {
        enumValue_ = value;
      }
    }

    /// <summary>Field number for the "string_value" field.</summary>
    public const int StringValueFieldNumber = 11;
    private string stringValue_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string StringValue {
      get { return stringValue_; }
      set {
        stringValue_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as StructType);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(StructType other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (BoolValue != other.BoolValue) return false;
      if (Int32Value != other.Int32Value) return false;
      if (Uint32Value != other.Uint32Value) return false;
      if (Int64Value != other.Int64Value) return false;
      if (Uint64Value != other.Uint64Value) return false;
      if (Sint32Value != other.Sint32Value) return false;
      if (Sint64Value != other.Sint64Value) return false;
      if (FloatValue != other.FloatValue) return false;
      if (DoubleValue != other.DoubleValue) return false;
      if (EnumValue != other.EnumValue) return false;
      if (StringValue != other.StringValue) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (BoolValue != false) hash ^= BoolValue.GetHashCode();
      if (Int32Value != 0) hash ^= Int32Value.GetHashCode();
      if (Uint32Value != 0) hash ^= Uint32Value.GetHashCode();
      if (Int64Value != 0L) hash ^= Int64Value.GetHashCode();
      if (Uint64Value != 0UL) hash ^= Uint64Value.GetHashCode();
      if (Sint32Value != 0) hash ^= Sint32Value.GetHashCode();
      if (Sint64Value != 0L) hash ^= Sint64Value.GetHashCode();
      if (FloatValue != 0F) hash ^= FloatValue.GetHashCode();
      if (DoubleValue != 0D) hash ^= DoubleValue.GetHashCode();
      if (EnumValue != 0) hash ^= EnumValue.GetHashCode();
      if (StringValue.Length != 0) hash ^= StringValue.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (BoolValue != false) {
        output.WriteRawTag(8);
        output.WriteBool(BoolValue);
      }
      if (Int32Value != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Int32Value);
      }
      if (Uint32Value != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(Uint32Value);
      }
      if (Int64Value != 0L) {
        output.WriteRawTag(32);
        output.WriteInt64(Int64Value);
      }
      if (Uint64Value != 0UL) {
        output.WriteRawTag(40);
        output.WriteUInt64(Uint64Value);
      }
      if (Sint32Value != 0) {
        output.WriteRawTag(48);
        output.WriteSInt32(Sint32Value);
      }
      if (Sint64Value != 0L) {
        output.WriteRawTag(56);
        output.WriteSInt64(Sint64Value);
      }
      if (FloatValue != 0F) {
        output.WriteRawTag(69);
        output.WriteFloat(FloatValue);
      }
      if (DoubleValue != 0D) {
        output.WriteRawTag(73);
        output.WriteDouble(DoubleValue);
      }
      if (EnumValue != 0) {
        output.WriteRawTag(80);
        output.WriteEnum((int) EnumValue);
      }
      if (StringValue.Length != 0) {
        output.WriteRawTag(90);
        output.WriteString(StringValue);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (BoolValue != false) {
        size += 1 + 1;
      }
      if (Int32Value != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Int32Value);
      }
      if (Uint32Value != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Uint32Value);
      }
      if (Int64Value != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Int64Value);
      }
      if (Uint64Value != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uint64Value);
      }
      if (Sint32Value != 0) {
        size += 1 + pb::CodedOutputStream.ComputeSInt32Size(Sint32Value);
      }
      if (Sint64Value != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeSInt64Size(Sint64Value);
      }
      if (FloatValue != 0F) {
        size += 1 + 4;
      }
      if (DoubleValue != 0D) {
        size += 1 + 8;
      }
      if (EnumValue != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) EnumValue);
      }
      if (StringValue.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(StringValue);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(StructType other) {
      if (other == null) {
        return;
      }
      if (other.BoolValue != false) {
        BoolValue = other.BoolValue;
      }
      if (other.Int32Value != 0) {
        Int32Value = other.Int32Value;
      }
      if (other.Uint32Value != 0) {
        Uint32Value = other.Uint32Value;
      }
      if (other.Int64Value != 0L) {
        Int64Value = other.Int64Value;
      }
      if (other.Uint64Value != 0UL) {
        Uint64Value = other.Uint64Value;
      }
      if (other.Sint32Value != 0) {
        Sint32Value = other.Sint32Value;
      }
      if (other.Sint64Value != 0L) {
        Sint64Value = other.Sint64Value;
      }
      if (other.FloatValue != 0F) {
        FloatValue = other.FloatValue;
      }
      if (other.DoubleValue != 0D) {
        DoubleValue = other.DoubleValue;
      }
      if (other.EnumValue != 0) {
        EnumValue = other.EnumValue;
      }
      if (other.StringValue.Length != 0) {
        StringValue = other.StringValue;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            BoolValue = input.ReadBool();
            break;
          }
          case 16: {
            Int32Value = input.ReadInt32();
            break;
          }
          case 24: {
            Uint32Value = input.ReadUInt32();
            break;
          }
          case 32: {
            Int64Value = input.ReadInt64();
            break;
          }
          case 40: {
            Uint64Value = input.ReadUInt64();
            break;
          }
          case 48: {
            Sint32Value = input.ReadSInt32();
            break;
          }
          case 56: {
            Sint64Value = input.ReadSInt64();
            break;
          }
          case 69: {
            FloatValue = input.ReadFloat();
            break;
          }
          case 73: {
            DoubleValue = input.ReadDouble();
            break;
          }
          case 80: {
            enumValue_ = (global::CodegenTest.EnumType) input.ReadEnum();
            break;
          }
          case 90: {
            StringValue = input.ReadString();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
