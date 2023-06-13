using System;

namespace RockIM.Sdk.Utils
{
    public abstract class DateUtils
    {
        public static long NowTs()
        {
            var now = DateTimeOffset.Now;
            return now.Millisecond;
        }
    }
}