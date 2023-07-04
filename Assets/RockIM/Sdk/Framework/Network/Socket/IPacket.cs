namespace RockIM.Sdk.Framework.Network.Socket
{
    public interface IPacket
    {
        public string ID { get; }

        public byte[] Data();
    }
}