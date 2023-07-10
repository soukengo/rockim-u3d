using Newtonsoft.Json;


namespace RockIM.Sdk.Utils
{
    public abstract class JsonUtils
    {
        public static string ToJson(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}