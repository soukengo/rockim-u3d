using System;
using System.Linq;
using Google.Protobuf.Reflection;

namespace RockIM.Sdk.Utils
{
    public abstract class ProtobufUtils
    {
        public static string GetEnumName<T>(T obj) where T : Enum
        {
            return obj.GetType().GetMember(obj.ToString()).FirstOrDefault()
                ?.GetCustomAttributes(typeof(OriginalNameAttribute), false)
                .OfType<OriginalNameAttribute>().FirstOrDefault()
                ?.Name;
        }
    }
}