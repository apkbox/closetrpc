// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: NanoMessenger.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace NanoMessanger {

  /// <summary>Holder for reflection information generated from NanoMessenger.proto</summary>
  public static partial class NanoMessengerReflection {

    #region Descriptor
    /// <summary>File descriptor for NanoMessenger.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static NanoMessengerReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNOYW5vTWVzc2VuZ2VyLnByb3RvEg5uYW5vX21lc3NhbmdlchobZ29vZ2xl",
            "L3Byb3RvYnVmL2VtcHR5LnByb3RvGh5nb29nbGUvcHJvdG9idWYvd3JhcHBl",
            "cnMucHJvdG8aFWNsb3NldHJwY190eXBlcy5wcm90byJqChBSZWdpc3RyYXRp",
            "b25EYXRhEhAKCHVzZXJuYW1lGAEgASgJEhAKCHBhc3N3b3JkGAIgASgJEg0K",
            "BWVtYWlsGAMgASgJEhEKCWZpcnN0TmFtZRgEIAEoCRIQCghsYXN0TmFtZRgF",
            "IAEoCSI4ChJBdXRoZW50aWNhdGlvbkRhdGESEAoIdXNlcm5hbWUYASABKAkS",
            "EAoIcGFzc3dvcmQYAiABKAkiIQoLU2Vzc2lvbkluZm8SEgoKc2Vzc2lvbktl",
            "eRgBIAEoCSJXCgtDb250YWN0SW5mbxIRCglmaXJzdE5hbWUYASABKAkSEAoI",
            "bGFzdE5hbWUYAiABKAkSEAoIdXNlcm5hbWUYAyABKAkSEQoJaXNfb25saW5l",
            "GAQgASgIIjwKC0NvbnRhY3RMaXN0Ei0KCENvbnRhY3RzGAEgAygLMhsubmFu",
            "b19tZXNzYW5nZXIuQ29udGFjdEluZm8iOgoHTWVzc2FnZRIQCgh1c2VybmFt",
            "ZRgBIAEoCRIMCgR0ZXh0GAIgASgJEg8KB29yZGluYWwYAyABKAUiOAoLTWVz",
            "c2FnZUxpc3QSKQoITWVzc2FnZXMYASADKAsyFy5uYW5vX21lc3Nhbmdlci5N",
            "ZXNzYWdlMlcKDVNlc3Npb25FdmVudHMSQAoOU2Vzc2lvbkV4cGlyZWQSFi5n",
            "b29nbGUucHJvdG9idWYuRW1wdHkaFi5nb29nbGUucHJvdG9idWYuRW1wdHka",
            "BOiLLAEygQMKDExvZ2luU2VydmljZRJICghSZWdpc3RlchIgLm5hbm9fbWVz",
            "c2FuZ2VyLlJlZ2lzdHJhdGlvbkRhdGEaGi5nb29nbGUucHJvdG9idWYuQm9v",
            "bFZhbHVlEkgKBUxvZ2luEiIubmFub19tZXNzYW5nZXIuQXV0aGVudGljYXRp",
            "b25EYXRhGhsubmFub19tZXNzYW5nZXIuU2Vzc2lvbkluZm8SPAoGTG9nb3V0",
            "EhYuZ29vZ2xlLnByb3RvYnVmLkVtcHR5GhouZ29vZ2xlLnByb3RvYnVmLkJv",
            "b2xWYWx1ZRJYChhDaGFuZ2VBdXRoZW50aWNhdGlvbkluZm8SIC5uYW5vX21l",
            "c3Nhbmdlci5SZWdpc3RyYXRpb25EYXRhGhouZ29vZ2xlLnByb3RvYnVmLkJv",
            "b2xWYWx1ZRJFCglSZWNvbm5lY3QSHC5nb29nbGUucHJvdG9idWYuU3RyaW5n",
            "VmFsdWUaGi5nb29nbGUucHJvdG9idWYuQm9vbFZhbHVlMqkCChJDb250YWN0",
            "TGlzdFNlcnZpY2USRQoIRmluZFVzZXISHC5nb29nbGUucHJvdG9idWYuU3Ry",
            "aW5nVmFsdWUaGy5uYW5vX21lc3Nhbmdlci5Db250YWN0TGlzdBJBCgpBZGRD",
            "b250YWN0EhsubmFub19tZXNzYW5nZXIuQ29udGFjdEluZm8aFi5nb29nbGUu",
            "cHJvdG9idWYuRW1wdHkSRQoNUmVtb3ZlQ29udGFjdBIcLmdvb2dsZS5wcm90",
            "b2J1Zi5TdHJpbmdWYWx1ZRoWLmdvb2dsZS5wcm90b2J1Zi5FbXB0eRJCCgtH",
            "ZXRDb250YWN0cxIWLmdvb2dsZS5wcm90b2J1Zi5FbXB0eRobLm5hbm9fbWVz",
            "c2FuZ2VyLkNvbnRhY3RMaXN0Mp8BCg1NZXNzYWdlRXZlbnRzEjwKCk5ld01l",
            "c3NhZ2USFi5nb29nbGUucHJvdG9idWYuRW1wdHkaFi5nb29nbGUucHJvdG9i",
            "dWYuRW1wdHkSSgoTT25saW5lU3RhdHVzQ2hhbmdlZBIbLm5hbm9fbWVzc2Fu",
            "Z2VyLkNvbnRhY3RJbmZvGhYuZ29vZ2xlLnByb3RvYnVmLkVtcHR5GgToiywB",
            "MqABCg5NZXNzYWdlU2VydmljZRI+CgtTZW5kTWVzc2FnZRIXLm5hbm9fbWVz",
            "c2FuZ2VyLk1lc3NhZ2UaFi5nb29nbGUucHJvdG9idWYuRW1wdHkSTgoSR2V0",
            "UGVuZGluZ01lc3NhZ2VzEhsuZ29vZ2xlLnByb3RvYnVmLkludDMyVmFsdWUa",
            "Gy5uYW5vX21lc3Nhbmdlci5NZXNzYWdlTGlzdEIQqgINTmFub01lc3Nhbmdl",
            "cmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.EmptyReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.WrappersReflection.Descriptor, global::ClosetRpc.ClosetrpcTypesReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::NanoMessanger.RegistrationData), global::NanoMessanger.RegistrationData.Parser, new[]{ "Username", "Password", "Email", "FirstName", "LastName" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::NanoMessanger.AuthenticationData), global::NanoMessanger.AuthenticationData.Parser, new[]{ "Username", "Password" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::NanoMessanger.SessionInfo), global::NanoMessanger.SessionInfo.Parser, new[]{ "SessionKey" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::NanoMessanger.ContactInfo), global::NanoMessanger.ContactInfo.Parser, new[]{ "FirstName", "LastName", "Username", "IsOnline" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::NanoMessanger.ContactList), global::NanoMessanger.ContactList.Parser, new[]{ "Contacts" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::NanoMessanger.Message), global::NanoMessanger.Message.Parser, new[]{ "Username", "Text", "Ordinal" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::NanoMessanger.MessageList), global::NanoMessanger.MessageList.Parser, new[]{ "Messages" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class RegistrationData : pb::IMessage<RegistrationData> {
    private static readonly pb::MessageParser<RegistrationData> _parser = new pb::MessageParser<RegistrationData>(() => new RegistrationData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RegistrationData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NanoMessanger.NanoMessengerReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegistrationData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegistrationData(RegistrationData other) : this() {
      username_ = other.username_;
      password_ = other.password_;
      email_ = other.email_;
      firstName_ = other.firstName_;
      lastName_ = other.lastName_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegistrationData Clone() {
      return new RegistrationData(this);
    }

    /// <summary>Field number for the "username" field.</summary>
    public const int UsernameFieldNumber = 1;
    private string username_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Username {
      get { return username_; }
      set {
        username_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "password" field.</summary>
    public const int PasswordFieldNumber = 2;
    private string password_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Password {
      get { return password_; }
      set {
        password_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "email" field.</summary>
    public const int EmailFieldNumber = 3;
    private string email_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Email {
      get { return email_; }
      set {
        email_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "firstName" field.</summary>
    public const int FirstNameFieldNumber = 4;
    private string firstName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string FirstName {
      get { return firstName_; }
      set {
        firstName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "lastName" field.</summary>
    public const int LastNameFieldNumber = 5;
    private string lastName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string LastName {
      get { return lastName_; }
      set {
        lastName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RegistrationData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RegistrationData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Username != other.Username) return false;
      if (Password != other.Password) return false;
      if (Email != other.Email) return false;
      if (FirstName != other.FirstName) return false;
      if (LastName != other.LastName) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Username.Length != 0) hash ^= Username.GetHashCode();
      if (Password.Length != 0) hash ^= Password.GetHashCode();
      if (Email.Length != 0) hash ^= Email.GetHashCode();
      if (FirstName.Length != 0) hash ^= FirstName.GetHashCode();
      if (LastName.Length != 0) hash ^= LastName.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Username.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Username);
      }
      if (Password.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Password);
      }
      if (Email.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Email);
      }
      if (FirstName.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(FirstName);
      }
      if (LastName.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(LastName);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Username.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Username);
      }
      if (Password.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
      }
      if (Email.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Email);
      }
      if (FirstName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FirstName);
      }
      if (LastName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(LastName);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RegistrationData other) {
      if (other == null) {
        return;
      }
      if (other.Username.Length != 0) {
        Username = other.Username;
      }
      if (other.Password.Length != 0) {
        Password = other.Password;
      }
      if (other.Email.Length != 0) {
        Email = other.Email;
      }
      if (other.FirstName.Length != 0) {
        FirstName = other.FirstName;
      }
      if (other.LastName.Length != 0) {
        LastName = other.LastName;
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
            Username = input.ReadString();
            break;
          }
          case 18: {
            Password = input.ReadString();
            break;
          }
          case 26: {
            Email = input.ReadString();
            break;
          }
          case 34: {
            FirstName = input.ReadString();
            break;
          }
          case 42: {
            LastName = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class AuthenticationData : pb::IMessage<AuthenticationData> {
    private static readonly pb::MessageParser<AuthenticationData> _parser = new pb::MessageParser<AuthenticationData>(() => new AuthenticationData());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<AuthenticationData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NanoMessanger.NanoMessengerReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AuthenticationData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AuthenticationData(AuthenticationData other) : this() {
      username_ = other.username_;
      password_ = other.password_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AuthenticationData Clone() {
      return new AuthenticationData(this);
    }

    /// <summary>Field number for the "username" field.</summary>
    public const int UsernameFieldNumber = 1;
    private string username_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Username {
      get { return username_; }
      set {
        username_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "password" field.</summary>
    public const int PasswordFieldNumber = 2;
    private string password_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Password {
      get { return password_; }
      set {
        password_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as AuthenticationData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(AuthenticationData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Username != other.Username) return false;
      if (Password != other.Password) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Username.Length != 0) hash ^= Username.GetHashCode();
      if (Password.Length != 0) hash ^= Password.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Username.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Username);
      }
      if (Password.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Password);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Username.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Username);
      }
      if (Password.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(AuthenticationData other) {
      if (other == null) {
        return;
      }
      if (other.Username.Length != 0) {
        Username = other.Username;
      }
      if (other.Password.Length != 0) {
        Password = other.Password;
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
            Username = input.ReadString();
            break;
          }
          case 18: {
            Password = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class SessionInfo : pb::IMessage<SessionInfo> {
    private static readonly pb::MessageParser<SessionInfo> _parser = new pb::MessageParser<SessionInfo>(() => new SessionInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SessionInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NanoMessanger.NanoMessengerReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SessionInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SessionInfo(SessionInfo other) : this() {
      sessionKey_ = other.sessionKey_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SessionInfo Clone() {
      return new SessionInfo(this);
    }

    /// <summary>Field number for the "sessionKey" field.</summary>
    public const int SessionKeyFieldNumber = 1;
    private string sessionKey_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string SessionKey {
      get { return sessionKey_; }
      set {
        sessionKey_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SessionInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SessionInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SessionKey != other.SessionKey) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (SessionKey.Length != 0) hash ^= SessionKey.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (SessionKey.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SessionKey);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (SessionKey.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SessionKey);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SessionInfo other) {
      if (other == null) {
        return;
      }
      if (other.SessionKey.Length != 0) {
        SessionKey = other.SessionKey;
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
            SessionKey = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class ContactInfo : pb::IMessage<ContactInfo> {
    private static readonly pb::MessageParser<ContactInfo> _parser = new pb::MessageParser<ContactInfo>(() => new ContactInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ContactInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NanoMessanger.NanoMessengerReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ContactInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ContactInfo(ContactInfo other) : this() {
      firstName_ = other.firstName_;
      lastName_ = other.lastName_;
      username_ = other.username_;
      isOnline_ = other.isOnline_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ContactInfo Clone() {
      return new ContactInfo(this);
    }

    /// <summary>Field number for the "firstName" field.</summary>
    public const int FirstNameFieldNumber = 1;
    private string firstName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string FirstName {
      get { return firstName_; }
      set {
        firstName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "lastName" field.</summary>
    public const int LastNameFieldNumber = 2;
    private string lastName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string LastName {
      get { return lastName_; }
      set {
        lastName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "username" field.</summary>
    public const int UsernameFieldNumber = 3;
    private string username_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Username {
      get { return username_; }
      set {
        username_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "is_online" field.</summary>
    public const int IsOnlineFieldNumber = 4;
    private bool isOnline_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsOnline {
      get { return isOnline_; }
      set {
        isOnline_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ContactInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ContactInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (FirstName != other.FirstName) return false;
      if (LastName != other.LastName) return false;
      if (Username != other.Username) return false;
      if (IsOnline != other.IsOnline) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (FirstName.Length != 0) hash ^= FirstName.GetHashCode();
      if (LastName.Length != 0) hash ^= LastName.GetHashCode();
      if (Username.Length != 0) hash ^= Username.GetHashCode();
      if (IsOnline != false) hash ^= IsOnline.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (FirstName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(FirstName);
      }
      if (LastName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(LastName);
      }
      if (Username.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Username);
      }
      if (IsOnline != false) {
        output.WriteRawTag(32);
        output.WriteBool(IsOnline);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (FirstName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(FirstName);
      }
      if (LastName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(LastName);
      }
      if (Username.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Username);
      }
      if (IsOnline != false) {
        size += 1 + 1;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ContactInfo other) {
      if (other == null) {
        return;
      }
      if (other.FirstName.Length != 0) {
        FirstName = other.FirstName;
      }
      if (other.LastName.Length != 0) {
        LastName = other.LastName;
      }
      if (other.Username.Length != 0) {
        Username = other.Username;
      }
      if (other.IsOnline != false) {
        IsOnline = other.IsOnline;
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
            FirstName = input.ReadString();
            break;
          }
          case 18: {
            LastName = input.ReadString();
            break;
          }
          case 26: {
            Username = input.ReadString();
            break;
          }
          case 32: {
            IsOnline = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  public sealed partial class ContactList : pb::IMessage<ContactList> {
    private static readonly pb::MessageParser<ContactList> _parser = new pb::MessageParser<ContactList>(() => new ContactList());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ContactList> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NanoMessanger.NanoMessengerReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ContactList() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ContactList(ContactList other) : this() {
      contacts_ = other.contacts_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ContactList Clone() {
      return new ContactList(this);
    }

    /// <summary>Field number for the "Contacts" field.</summary>
    public const int ContactsFieldNumber = 1;
    private static readonly pb::FieldCodec<global::NanoMessanger.ContactInfo> _repeated_contacts_codec
        = pb::FieldCodec.ForMessage(10, global::NanoMessanger.ContactInfo.Parser);
    private readonly pbc::RepeatedField<global::NanoMessanger.ContactInfo> contacts_ = new pbc::RepeatedField<global::NanoMessanger.ContactInfo>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::NanoMessanger.ContactInfo> Contacts {
      get { return contacts_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ContactList);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ContactList other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!contacts_.Equals(other.contacts_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= contacts_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      contacts_.WriteTo(output, _repeated_contacts_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += contacts_.CalculateSize(_repeated_contacts_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ContactList other) {
      if (other == null) {
        return;
      }
      contacts_.Add(other.contacts_);
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
            contacts_.AddEntriesFrom(input, _repeated_contacts_codec);
            break;
          }
        }
      }
    }

  }

  public sealed partial class Message : pb::IMessage<Message> {
    private static readonly pb::MessageParser<Message> _parser = new pb::MessageParser<Message>(() => new Message());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Message> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NanoMessanger.NanoMessengerReflection.Descriptor.MessageTypes[5]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Message() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Message(Message other) : this() {
      username_ = other.username_;
      text_ = other.text_;
      ordinal_ = other.ordinal_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Message Clone() {
      return new Message(this);
    }

    /// <summary>Field number for the "username" field.</summary>
    public const int UsernameFieldNumber = 1;
    private string username_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Username {
      get { return username_; }
      set {
        username_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "text" field.</summary>
    public const int TextFieldNumber = 2;
    private string text_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Text {
      get { return text_; }
      set {
        text_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "ordinal" field.</summary>
    public const int OrdinalFieldNumber = 3;
    private int ordinal_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Ordinal {
      get { return ordinal_; }
      set {
        ordinal_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Message);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Message other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Username != other.Username) return false;
      if (Text != other.Text) return false;
      if (Ordinal != other.Ordinal) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Username.Length != 0) hash ^= Username.GetHashCode();
      if (Text.Length != 0) hash ^= Text.GetHashCode();
      if (Ordinal != 0) hash ^= Ordinal.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Username.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Username);
      }
      if (Text.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Text);
      }
      if (Ordinal != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Ordinal);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Username.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Username);
      }
      if (Text.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Text);
      }
      if (Ordinal != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Ordinal);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Message other) {
      if (other == null) {
        return;
      }
      if (other.Username.Length != 0) {
        Username = other.Username;
      }
      if (other.Text.Length != 0) {
        Text = other.Text;
      }
      if (other.Ordinal != 0) {
        Ordinal = other.Ordinal;
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
            Username = input.ReadString();
            break;
          }
          case 18: {
            Text = input.ReadString();
            break;
          }
          case 24: {
            Ordinal = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class MessageList : pb::IMessage<MessageList> {
    private static readonly pb::MessageParser<MessageList> _parser = new pb::MessageParser<MessageList>(() => new MessageList());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MessageList> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::NanoMessanger.NanoMessengerReflection.Descriptor.MessageTypes[6]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageList() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageList(MessageList other) : this() {
      messages_ = other.messages_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MessageList Clone() {
      return new MessageList(this);
    }

    /// <summary>Field number for the "Messages" field.</summary>
    public const int MessagesFieldNumber = 1;
    private static readonly pb::FieldCodec<global::NanoMessanger.Message> _repeated_messages_codec
        = pb::FieldCodec.ForMessage(10, global::NanoMessanger.Message.Parser);
    private readonly pbc::RepeatedField<global::NanoMessanger.Message> messages_ = new pbc::RepeatedField<global::NanoMessanger.Message>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::NanoMessanger.Message> Messages {
      get { return messages_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MessageList);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MessageList other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!messages_.Equals(other.messages_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= messages_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      messages_.WriteTo(output, _repeated_messages_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += messages_.CalculateSize(_repeated_messages_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MessageList other) {
      if (other == null) {
        return;
      }
      messages_.Add(other.messages_);
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
            messages_.AddEntriesFrom(input, _repeated_messages_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
