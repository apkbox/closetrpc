// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: closetrpc_types.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ClosetRpc {

  /// <summary>Holder for reflection information generated from closetrpc_types.proto</summary>
  public static partial class ClosetrpcTypesReflection {

    #region Descriptor
    /// <summary>File descriptor for closetrpc_types.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ClosetrpcTypesReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVjbG9zZXRycGNfdHlwZXMucHJvdG8SB25hbm9ycGMaIGdvb2dsZS9wcm90",
            "b2J1Zi9kZXNjcmlwdG9yLnByb3RvIiAKD1dpZGVTdHJpbmdWYWx1ZRINCgV2",
            "YWx1ZRgBIAEoCSIcCgtTSW50MzJWYWx1ZRINCgV2YWx1ZRgBIAEoESIcCgtT",
            "SW50NjRWYWx1ZRINCgV2YWx1ZRgBIAEoEiIeCglScGNPYmplY3QSEQoJb2Jq",
            "ZWN0X2lkGAEgASgEOj4KE2V4cGFuZF9hc19hcmd1bWVudHMSHy5nb29nbGUu",
            "cHJvdG9idWYuTWVzc2FnZU9wdGlvbnMYkb8FIAEoCDo3CgxlbnVtX3dyYXBw",
            "ZXISHy5nb29nbGUucHJvdG9idWYuTWVzc2FnZU9wdGlvbnMYkr8FIAEoCDo3",
            "CgxldmVudF9zb3VyY2USHy5nb29nbGUucHJvdG9idWYuU2VydmljZU9wdGlv",
            "bnMYvcEFIAEoCDo1Cgtpc19wcm9wZXJ0eRIeLmdvb2dsZS5wcm90b2J1Zi5N",
            "ZXRob2RPcHRpb25zGKHCBSABKAg6LwoFYXN5bmMSHi5nb29nbGUucHJvdG9i",
            "dWYuTWV0aG9kT3B0aW9ucxiiwgUgASgIQgyqAglDbG9zZXRScGNiBnByb3Rv",
            "Mw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { pbr::FileDescriptor.DescriptorProtoFileDescriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ClosetRpc.WideStringValue), global::ClosetRpc.WideStringValue.Parser, new[]{ "Value" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClosetRpc.SInt32Value), global::ClosetRpc.SInt32Value.Parser, new[]{ "Value" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClosetRpc.SInt64Value), global::ClosetRpc.SInt64Value.Parser, new[]{ "Value" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClosetRpc.RpcObject), global::ClosetRpc.RpcObject.Parser, new[]{ "ObjectId" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class WideStringValue : pb::IMessage<WideStringValue> {
    private static readonly pb::MessageParser<WideStringValue> _parser = new pb::MessageParser<WideStringValue>(() => new WideStringValue());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<WideStringValue> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClosetRpc.ClosetrpcTypesReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WideStringValue() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WideStringValue(WideStringValue other) : this() {
      value_ = other.value_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WideStringValue Clone() {
      return new WideStringValue(this);
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 1;
    private string value_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Value {
      get { return value_; }
      set {
        value_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as WideStringValue);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(WideStringValue other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Value != other.Value) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Value.Length != 0) hash ^= Value.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Value.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Value);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Value.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Value);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(WideStringValue other) {
      if (other == null) {
        return;
      }
      if (other.Value.Length != 0) {
        Value = other.Value;
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
          case 10: {
            Value = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class SInt32Value : pb::IMessage<SInt32Value> {
    private static readonly pb::MessageParser<SInt32Value> _parser = new pb::MessageParser<SInt32Value>(() => new SInt32Value());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SInt32Value> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClosetRpc.ClosetrpcTypesReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SInt32Value() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SInt32Value(SInt32Value other) : this() {
      value_ = other.value_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SInt32Value Clone() {
      return new SInt32Value(this);
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 1;
    private int value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SInt32Value);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SInt32Value other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Value != other.Value) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Value != 0) hash ^= Value.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Value != 0) {
        output.WriteRawTag(8);
        output.WriteSInt32(Value);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Value != 0) {
        size += 1 + pb::CodedOutputStream.ComputeSInt32Size(Value);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SInt32Value other) {
      if (other == null) {
        return;
      }
      if (other.Value != 0) {
        Value = other.Value;
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
            Value = input.ReadSInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class SInt64Value : pb::IMessage<SInt64Value> {
    private static readonly pb::MessageParser<SInt64Value> _parser = new pb::MessageParser<SInt64Value>(() => new SInt64Value());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SInt64Value> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClosetRpc.ClosetrpcTypesReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SInt64Value() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SInt64Value(SInt64Value other) : this() {
      value_ = other.value_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SInt64Value Clone() {
      return new SInt64Value(this);
    }

    /// <summary>Field number for the "value" field.</summary>
    public const int ValueFieldNumber = 1;
    private long value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SInt64Value);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SInt64Value other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Value != other.Value) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Value != 0L) hash ^= Value.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Value != 0L) {
        output.WriteRawTag(8);
        output.WriteSInt64(Value);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Value != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeSInt64Size(Value);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SInt64Value other) {
      if (other == null) {
        return;
      }
      if (other.Value != 0L) {
        Value = other.Value;
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
            Value = input.ReadSInt64();
            break;
          }
        }
      }
    }

  }

  public sealed partial class RpcObject : pb::IMessage<RpcObject> {
    private static readonly pb::MessageParser<RpcObject> _parser = new pb::MessageParser<RpcObject>(() => new RpcObject());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RpcObject> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClosetRpc.ClosetrpcTypesReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RpcObject() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RpcObject(RpcObject other) : this() {
      objectId_ = other.objectId_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RpcObject Clone() {
      return new RpcObject(this);
    }

    /// <summary>Field number for the "object_id" field.</summary>
    public const int ObjectIdFieldNumber = 1;
    private ulong objectId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ulong ObjectId {
      get { return objectId_; }
      set {
        objectId_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RpcObject);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RpcObject other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ObjectId != other.ObjectId) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (ObjectId != 0UL) hash ^= ObjectId.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (ObjectId != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(ObjectId);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ObjectId != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(ObjectId);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RpcObject other) {
      if (other == null) {
        return;
      }
      if (other.ObjectId != 0UL) {
        ObjectId = other.ObjectId;
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
            ObjectId = input.ReadUInt64();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code