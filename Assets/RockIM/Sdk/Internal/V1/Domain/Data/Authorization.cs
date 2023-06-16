namespace RockIM.Sdk.Internal.V1.Domain.Data
{
    public class Authorization
    {
        public readonly string AccessToken;

        public Authorization(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}