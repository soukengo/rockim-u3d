namespace RockIM.Sdk.Internal.V1.Context
{
    public class Authorization
    {
        public string AccessToken;

        public Authorization(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}