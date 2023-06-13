// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: rockim/shared/enums/group.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace RockIM.Shared.Enums {

  /// <summary>Holder for reflection information generated from rockim/shared/enums/group.proto</summary>
  public static partial class GroupReflection {

    #region Descriptor
    /// <summary>File descriptor for rockim/shared/enums/group.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GroupReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch9yb2NraW0vc2hhcmVkL2VudW1zL2dyb3VwLnByb3RvEhNyb2NraW0uc2hh",
            "cmVkLmVudW1zIoMBCgVHcm91cCIjCghDYXRlZ29yeRIMCghDSEFUUk9PTRAA",
            "EgkKBUdST1VQEAEiMgoKTWVtYmVyUm9sZRIMCghPUkRJTkFSWRAAEgsKB01B",
            "TkFHRVIQARIJCgVPV05FUhADIiEKBlN0YXR1cxIKCgZOT1JNQUwQABILCgdE",
            "RUxFVEVEEAFCVwoWY24ucm9ja2ltLnNoYXJlZC5lbm11c1ABWiVyb2NraW1z",
            "ZXJ2ZXIvYXBpcy9yb2NraW0vc2hhcmVkL2VudW1zqgITUm9ja0lNLlNoYXJl",
            "ZC5FbnVtc2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::RockIM.Shared.Enums.Group), global::RockIM.Shared.Enums.Group.Parser, null, null, new[]{ typeof(global::RockIM.Shared.Enums.Group.Types.Category), typeof(global::RockIM.Shared.Enums.Group.Types.MemberRole), typeof(global::RockIM.Shared.Enums.Group.Types.Status) }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  /// Group 群组相关枚举
  /// </summary>
  public sealed partial class Group : pb::IMessage<Group>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Group> _parser = new pb::MessageParser<Group>(() => new Group());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Group> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::RockIM.Shared.Enums.GroupReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Group() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Group(Group other) : this() {
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Group Clone() {
      return new Group(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Group);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Group other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
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
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Group other) {
      if (other == null) {
        return;
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
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the Group message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      /// <summary>
      /// 群组分类
      /// </summary>
      public enum Category {
        /// <summary>
        /// 聊天室
        /// </summary>
        [pbr::OriginalName("CHATROOM")] Chatroom = 0,
        /// <summary>
        /// 普通群
        /// </summary>
        [pbr::OriginalName("GROUP")] Group = 1,
      }

      /// <summary>
      /// 成员角色
      /// </summary>
      public enum MemberRole {
        /// <summary>
        /// 普通用户
        /// </summary>
        [pbr::OriginalName("ORDINARY")] Ordinary = 0,
        /// <summary>
        /// 管理员
        /// </summary>
        [pbr::OriginalName("MANAGER")] Manager = 1,
        /// <summary>
        /// 群主
        /// </summary>
        [pbr::OriginalName("OWNER")] Owner = 3,
      }

      /// <summary>
      /// 状态
      /// </summary>
      public enum Status {
        /// <summary>
        /// 正常
        /// </summary>
        [pbr::OriginalName("NORMAL")] Normal = 0,
        /// <summary>
        /// 已删除
        /// </summary>
        [pbr::OriginalName("DELETED")] Deleted = 1,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
