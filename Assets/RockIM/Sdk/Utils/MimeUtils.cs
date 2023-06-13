namespace RockIM.Sdk.Utils
{
    public abstract class MimeUtils
    {
        public const string Json = Application + Sep + JsonSuffix;
        public const string ProtobufX = Application + Sep + ProtobufXSuffix;
        public const string Protobuf = Application + Sep + ProtobufSuffix;


        private const string Sep = "/";
        private const string Application = "application";
        private const string JsonSuffix = "json";
        private const string ProtobufSuffix = "proto";
        private const string ProtobufXSuffix = "x-protobuf";

        public static bool IsJson(string contentType)
        {
            var subType = ContentSubtype(contentType);
            return JsonSuffix == subType;
        }

        public static bool IsProtobuf(string contentType)
        {
            var subType = ContentSubtype(contentType);
            return subType is ProtobufSuffix or ProtobufXSuffix;
        }

        private static string ContentSubtype(string contentType)
        {
            int left = contentType.IndexOf('/');
            if (left == -1)
            {
                return "";
            }

            int right = contentType.IndexOf(';');
            if (right == -1)
            {
                right = contentType.Length;
            }

            if (right < left)
            {
                return "";
            }

            return contentType.Substring(left + 1, right - left - 1);
        }
    }
}