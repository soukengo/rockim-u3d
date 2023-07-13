namespace RockIM.Sdk.Internal.V1.Infra
{
    public abstract class Action
    {
        public const string Config = "/client/v1/product/config/fetch";

        public const string Login = "/client/v1/auth/login";

        public const string MessageSend = "/client/v1/message/send";

        public const string ChatRoomJoin = "/client/v1/chatroom/member/join";
    }
}