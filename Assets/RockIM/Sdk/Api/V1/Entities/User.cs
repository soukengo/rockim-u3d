using System.Collections.Generic;

namespace RockIM.Sdk.Api.V1.Entities
{
    public class User
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public Dictionary<string, string> Fields { get; set; }
    }
}