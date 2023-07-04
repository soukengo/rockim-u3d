using System;

namespace RockIM.Sdk.Utils
{
    public abstract class DateUtils
    {
        public static long NowTs()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
    }
}