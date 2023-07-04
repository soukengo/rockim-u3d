using System.IO;

namespace RockIM.Sdk.Framework.Network.Socket
{
    public interface IPacketParser<out T> where T : IPacket
    {
        T Parse(byte[] source);
        T Parse(Stream ins);
    }
}