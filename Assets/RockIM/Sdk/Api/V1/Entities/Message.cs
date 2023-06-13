using RockIM.Sdk.Api.V1.Enums;

namespace RockIM.Sdk.Internal.V1.Domain.Models.Entities
{
    public class Message
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public string Content { get; set; }
    }
}