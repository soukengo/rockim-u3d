using System;
using System.IO;
using RockIM.Api.Client.V1.Protocol.Socket;
using RockIM.Sdk.Framework.Network.Socket;
using RockIM.Shared.Enums;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public class Packet : IPacket
    {
        private const int VersionSize = 2;
        private const int TypeSize = 1;
        private const int HeaderLenSize = 2;
        private const int BodyLenSize = 4;
        private const int VersionOffset = 0;

        private const int TypeOffset = VersionOffset + VersionSize;
        private const int HeaderLenOffset = TypeOffset + TypeSize;
        private const int BodyLenOffset = HeaderLenOffset + HeaderLenSize;
        private const int HeaderOffset = BodyLenOffset + BodyLenSize;
        private const int PacketHeaderSize = VersionSize + TypeSize + HeaderLenSize + BodyLenSize;

        public ushort Version { get; }

        public byte Type { get; }

        public byte[] Header { get; }

        public byte[] Body { get; }

        public string ID { get; set; }

        public ResponsePacketHeader ResponseHeader { get; private set; }

        public Packet(ushort version, byte type, byte[] header, byte[] body) :
            this("", version, type, header, body)
        {
        }

        public Packet(string id, ushort version, byte type, byte[] header, byte[] body)
        {
            ID = id;
            Version = version;
            Type = type;
            Header = header;
            Body = body;
        }


        public byte[] Data()
        {
            var data = new byte[PacketHeaderSize + Header.Length + Body.Length];
            BitConverter.GetBytes(Version).CopyTo(data, VersionOffset);
            data[TypeOffset] = Type;
            BitConverter.GetBytes(Header.Length).CopyTo(data, HeaderLenOffset);
            BitConverter.GetBytes(Body.Length).CopyTo(data, BodyLenOffset);

            var bodyOffset = HeaderOffset;

            if (Header is {Length: > 0})
            {
                Header.CopyTo(data, HeaderOffset);
                bodyOffset = HeaderOffset + +Header.Length;
            }

            if (Body is {Length: > 0})
            {
                Body.CopyTo(data, bodyOffset);
            }

            return data;
        }

        public static Packet ReadFrom(Stream ins)
        {
            if (ins.Length == 0)
            {
                return null;
            }

            var packetHeader = new byte[PacketHeaderSize];
            var packetHeaderLen = ins.Read(packetHeader);
            if (packetHeaderLen != PacketHeaderSize)
            {
                return null;
            }

            var version = BitConverter.ToUInt16(new ArraySegment<byte>(packetHeader, VersionOffset, TypeOffset));
            var type = packetHeader[TypeOffset];
            var headerLen = BitConverter.ToUInt16(new ArraySegment<byte>(packetHeader, HeaderLenOffset, BodyLenOffset));
            var bodyLen = BitConverter.ToUInt16(new ArraySegment<byte>(packetHeader, BodyLenOffset, HeaderOffset));

            var dataLen = headerLen + bodyLen;
            var bodyOffset = HeaderOffset + headerLen;
            var lastOffset = bodyOffset + bodyLen;

            var data = new byte[dataLen];
            var dataLenRead = ins.Read(data);
            if (dataLenRead != dataLen)
            {
                return null;
            }

            byte[] header = { };
            byte[] body = { };
            if (headerLen > 0)
            {
                header = new ArraySegment<byte>(packetHeader, HeaderOffset, bodyOffset).ToArray();
            }

            if (bodyLen > 0)
            {
                body = new ArraySegment<byte>(packetHeader, bodyOffset, lastOffset).ToArray();
            }

            var packet = new Packet(version, type, header, body);
            if (Network.Types.PacketType.Response == (Network.Types.PacketType) type)
            {
                var respHeader = Codec.Decode<ResponsePacketHeader>(header);
                packet.ID = respHeader.RequestId.ToString();
                packet.ResponseHeader = respHeader;
            }

            return packet;
        }
    }
}