using Google.Protobuf;

namespace RockIM.Sdk.Internal.V1.Infra.Socket
{
    public abstract class Codec
    {
        public static byte[] Encode(IMessage message)
        {
            return message.ToByteArray();
        }

        public static T Decode<T>(byte[] data) where T : IMessage, new()
        {
            var message = new T();
            message.MergeFrom(data);
            return message;
        }
    }
}