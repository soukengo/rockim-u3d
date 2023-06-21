using System.Collections.Generic;
using RockIM.Sdk.Api.V1.Entities;
using RockIM.Sdk.Internal.V1.Domain.Entities;

namespace RockIM.Sdk.Internal.V1.Infra.Converter
{
    public abstract class UserConvert
    {
        public static User Convert(RockIM.Api.Client.V1.Types.User source)
        {
            var ret = new User
            {
                Account = source.Account,
                Name = source.Name,
                AvatarUrl = source.AvatarUrl,
                Fields = new Dictionary<string, string>(),
            };
            if (source.Fields == null)
            {
                return ret;
            }

            foreach (var item in source.Fields)
            {
                source.Fields[item.Key] = item.Value;
            }

            return ret;
        }
    }
}