// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: RpcInvocation.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from RpcInvocation.proto</summary>
public static partial class RpcInvocationReflection {

  #region Descriptor
  /// <summary>File descriptor for RpcInvocation.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static RpcInvocationReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChNScGNJbnZvY2F0aW9uLnByb3RvIkAKE1JwY0ludm9jYXRpb25IZWFkZXIS",
          "DAoETmFtZRgBIAEoCRIKCgJJZBgCIAEoBRIPCgdOdW1BcmdzGAMgASgFIkcK",
          "DlByaW1pdGl2ZVZhbHVlEhQKCkludDMyVmFsdWUYASABKAVIABIVCgtTdHJp",
          "bmdWYWx1ZRgCIAEoCUgAQggKBm9uZW9mX2IGcHJvdG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::RpcInvocationHeader), global::RpcInvocationHeader.Parser, new[]{ "Name", "Id", "NumArgs" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::PrimitiveValue), global::PrimitiveValue.Parser, new[]{ "Int32Value", "StringValue" }, new[]{ "Oneof" }, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class RpcInvocationHeader : pb::IMessage<RpcInvocationHeader> {
  private static readonly pb::MessageParser<RpcInvocationHeader> _parser = new pb::MessageParser<RpcInvocationHeader>(() => new RpcInvocationHeader());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<RpcInvocationHeader> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::RpcInvocationReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public RpcInvocationHeader() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public RpcInvocationHeader(RpcInvocationHeader other) : this() {
    name_ = other.name_;
    id_ = other.id_;
    numArgs_ = other.numArgs_;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public RpcInvocationHeader Clone() {
    return new RpcInvocationHeader(this);
  }

  /// <summary>Field number for the "Name" field.</summary>
  public const int NameFieldNumber = 1;
  private string name_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Name {
    get { return name_; }
    set {
      name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "Id" field.</summary>
  public const int IdFieldNumber = 2;
  private int id_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Id {
    get { return id_; }
    set {
      id_ = value;
    }
  }

  /// <summary>Field number for the "NumArgs" field.</summary>
  public const int NumArgsFieldNumber = 3;
  private int numArgs_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int NumArgs {
    get { return numArgs_; }
    set {
      numArgs_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as RpcInvocationHeader);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(RpcInvocationHeader other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Name != other.Name) return false;
    if (Id != other.Id) return false;
    if (NumArgs != other.NumArgs) return false;
    return true;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Name.Length != 0) hash ^= Name.GetHashCode();
    if (Id != 0) hash ^= Id.GetHashCode();
    if (NumArgs != 0) hash ^= NumArgs.GetHashCode();
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Name.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(Name);
    }
    if (Id != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(Id);
    }
    if (NumArgs != 0) {
      output.WriteRawTag(24);
      output.WriteInt32(NumArgs);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Name.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
    }
    if (Id != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
    }
    if (NumArgs != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(NumArgs);
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(RpcInvocationHeader other) {
    if (other == null) {
      return;
    }
    if (other.Name.Length != 0) {
      Name = other.Name;
    }
    if (other.Id != 0) {
      Id = other.Id;
    }
    if (other.NumArgs != 0) {
      NumArgs = other.NumArgs;
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
          Name = input.ReadString();
          break;
        }
        case 16: {
          Id = input.ReadInt32();
          break;
        }
        case 24: {
          NumArgs = input.ReadInt32();
          break;
        }
      }
    }
  }

}

public sealed partial class PrimitiveValue : pb::IMessage<PrimitiveValue> {
  private static readonly pb::MessageParser<PrimitiveValue> _parser = new pb::MessageParser<PrimitiveValue>(() => new PrimitiveValue());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<PrimitiveValue> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::RpcInvocationReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PrimitiveValue() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PrimitiveValue(PrimitiveValue other) : this() {
    switch (other.OneofCase) {
      case OneofOneofCase.Int32Value:
        Int32Value = other.Int32Value;
        break;
      case OneofOneofCase.StringValue:
        StringValue = other.StringValue;
        break;
    }

  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PrimitiveValue Clone() {
    return new PrimitiveValue(this);
  }

  /// <summary>Field number for the "Int32Value" field.</summary>
  public const int Int32ValueFieldNumber = 1;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Int32Value {
    get { return oneofCase_ == OneofOneofCase.Int32Value ? (int) oneof_ : 0; }
    set {
      oneof_ = value;
      oneofCase_ = OneofOneofCase.Int32Value;
    }
  }

  /// <summary>Field number for the "StringValue" field.</summary>
  public const int StringValueFieldNumber = 2;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string StringValue {
    get { return oneofCase_ == OneofOneofCase.StringValue ? (string) oneof_ : ""; }
    set {
      oneof_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      oneofCase_ = OneofOneofCase.StringValue;
    }
  }

  private object oneof_;
  /// <summary>Enum of possible cases for the "oneof_" oneof.</summary>
  public enum OneofOneofCase {
    None = 0,
    Int32Value = 1,
    StringValue = 2,
  }
  private OneofOneofCase oneofCase_ = OneofOneofCase.None;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public OneofOneofCase OneofCase {
    get { return oneofCase_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void ClearOneof() {
    oneofCase_ = OneofOneofCase.None;
    oneof_ = null;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as PrimitiveValue);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(PrimitiveValue other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Int32Value != other.Int32Value) return false;
    if (StringValue != other.StringValue) return false;
    if (OneofCase != other.OneofCase) return false;
    return true;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (oneofCase_ == OneofOneofCase.Int32Value) hash ^= Int32Value.GetHashCode();
    if (oneofCase_ == OneofOneofCase.StringValue) hash ^= StringValue.GetHashCode();
    hash ^= (int) oneofCase_;
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (oneofCase_ == OneofOneofCase.Int32Value) {
      output.WriteRawTag(8);
      output.WriteInt32(Int32Value);
    }
    if (oneofCase_ == OneofOneofCase.StringValue) {
      output.WriteRawTag(18);
      output.WriteString(StringValue);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (oneofCase_ == OneofOneofCase.Int32Value) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Int32Value);
    }
    if (oneofCase_ == OneofOneofCase.StringValue) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(StringValue);
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(PrimitiveValue other) {
    if (other == null) {
      return;
    }
    switch (other.OneofCase) {
      case OneofOneofCase.Int32Value:
        Int32Value = other.Int32Value;
        break;
      case OneofOneofCase.StringValue:
        StringValue = other.StringValue;
        break;
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
          Int32Value = input.ReadInt32();
          break;
        }
        case 18: {
          StringValue = input.ReadString();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code