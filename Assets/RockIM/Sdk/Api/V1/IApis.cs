using RockIM.Sdk.Api.v1;

namespace RockIM.Sdk.Api.V1
{
    public interface IApis
    {
        public IAuthApi Auth();

        public IAuthorizedApis Authorized();
    }
}