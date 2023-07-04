using System.IO;
using RockIM.Sdk.Framework.Network.Socket;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public class PacketParser : IPacketParser<Packet>
    {
        public Packet Parse(byte[] source)
        {
            var stream = new MemoryStream();
            try
            {
                return Parse(stream);
            }
            finally
            {
                stream.Close();
            }
        }

        public Packet Parse(Stream ins)
        {
            return Packet.ReadFrom(ins);
        }
    }
}