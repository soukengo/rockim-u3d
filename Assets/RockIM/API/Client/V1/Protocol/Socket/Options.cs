// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: rockim/api/client/v1/protocol/socket/options.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace RockIM.Api.Client.V1.Protocol.Socket {

  /// <summary>Holder for reflection information generated from rockim/api/client/v1/protocol/socket/options.proto</summary>
  public static partial class OptionsReflection {

    #region Descriptor
    /// <summary>File descriptor for rockim/api/client/v1/protocol/socket/options.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static OptionsReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjJyb2NraW0vYXBpL2NsaWVudC92MS9wcm90b2NvbC9zb2NrZXQvb3B0aW9u",
            "cy5wcm90bxIkcm9ja2ltLmFwaS5jbGllbnQudjEucHJvdG9jb2wuc29ja2V0",
            "GiBnb29nbGUvcHJvdG9idWYvZGVzY3JpcHRvci5wcm90bxozcm9ja2ltL2Fw",
            "aS9jbGllbnQvdjEvcHJvdG9jb2wvc29ja2V0L3Byb3RvY29sLnByb3RvIlQK",
            "DlJlcXVlc3RPcHRpb25zEkIKCW9wZXJhdGlvbhgBIAEoDjIvLnJvY2tpbS5h",
            "cGkuY2xpZW50LnYxLnByb3RvY29sLnNvY2tldC5PcGVyYXRpb246ZgoHcmVx",
            "dWVzdBIeLmdvb2dsZS5wcm90b2J1Zi5NZXRob2RPcHRpb25zGLkXIAEoCzI0",
            "LnJvY2tpbS5hcGkuY2xpZW50LnYxLnByb3RvY29sLnNvY2tldC5SZXF1ZXN0",
            "T3B0aW9uc0JmWj1yb2NraW1zZXJ2ZXIvYXBpcy9yb2NraW0vYXBpL2NsaWVu",
            "dC92MS9wcm90b2NvbC9zb2NrZXQ7c29ja2V0qgIkUm9ja0lNLkFwaS5DbGll",
            "bnQuVjEuUHJvdG9jb2wuU29ja2V0YgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.Reflection.DescriptorReflection.Descriptor, global::RockIM.Api.Client.V1.Protocol.Socket.ProtocolReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pb::Extension[] { OptionsExtensions.Request }, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::RockIM.Api.Client.V1.Protocol.Socket.RequestOptions), global::RockIM.Api.Client.V1.Protocol.Socket.RequestOptions.Parser, new[]{ "Operation" }, null, null, null, null)
          }));
    }
    #endregion

  }
  /// <summary>Holder for extension identifiers generated from the top level of rockim/api/client/v1/protocol/socket/options.proto</summary>
  public static partial class OptionsExtensions {
    public static readonly pb::Extension<global::Google.Protobuf.Reflection.MethodOptions, global::RockIM.Api.Client.V1.Protocol.Socket.RequestOptions> Request =
      new pb::Extension<global::Google.Protobuf.Reflection.MethodOptions, global::RockIM.Api.Client.V1.Protocol.Socket.RequestOptions>(3001, pb::FieldCodec.ForMessage(24010, global::RockIM.Api.Client.V1.Protocol.Socket.RequestOptions.Parser));
  }

  #region Messages
  public sealed partial class RequestOptions : pb::IMessage<RequestOptions>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<RequestOptions> _parser = new pb::MessageParser<RequestOptions>(() => new RequestOptions());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<RequestOptions> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::RockIM.Api.Client.V1.Protocol.Socket.OptionsReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RequestOptions() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RequestOptions(RequestOptions other) : this() {
      operation_ = other.operation_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RequestOptions Clone() {
      return new RequestOptions(this);
    }

    /// <summary>Field number for the "operation" field.</summary>
    public const int OperationFieldNumber = 1;
    private global::RockIM.Api.Client.V1.Protocol.Socket.Operation operation_ = global::RockIM.Api.Client.V1.Protocol.Socket.Operation.InvalidRequest;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::RockIM.Api.Client.V1.Protocol.Socket.Operation Operation {
      get { return operation_; }
      set {
        operation_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as RequestOptions);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(RequestOptions other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Operation != other.Operation) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Operation != global::RockIM.Api.Client.V1.Protocol.Socket.Operation.InvalidRequest) hash ^= Operation.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Operation != global::RockIM.Api.Client.V1.Protocol.Socket.Operation.InvalidRequest) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Operation);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Operation != global::RockIM.Api.Client.V1.Protocol.Socket.Operation.InvalidRequest) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Operation);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Operation != global::RockIM.Api.Client.V1.Protocol.Socket.Operation.InvalidRequest) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Operation);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(RequestOptions other) {
      if (other == null) {
        return;
      }
      if (other.Operation != global::RockIM.Api.Client.V1.Protocol.Socket.Operation.InvalidRequest) {
        Operation = other.Operation;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Operation = (global::RockIM.Api.Client.V1.Protocol.Socket.Operation) input.ReadEnum();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Operation = (global::RockIM.Api.Client.V1.Protocol.Socket.Operation) input.ReadEnum();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code