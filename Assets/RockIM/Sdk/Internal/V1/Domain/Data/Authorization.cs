using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Internal.V1.Domain.Entities;

namespace RockIM.Sdk.Internal.V1.Domain.Data
{
    public class Authorization
    {
        public readonly string AccessToken;

        public readonly User User;

        public Authorization(string accessToken, User user)
        {
            AccessToken = accessToken;
            User = user;
        }
    }
}