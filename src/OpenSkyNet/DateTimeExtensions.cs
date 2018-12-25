using System;

namespace OpenSkyNet
{
    static class DateTimeExtensions
    {
        static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static int ToUnixTimestamp(this DateTime time)
        {
            var result = (int)time.ToUniversalTime().Subtract(epoch).TotalSeconds;
            if (result < 0)
                return -1;

            return result;
        }

        public static int? ToUnixTimestamp(this DateTime? time)
        {
            if (time == null)
                return null;

            return time.Value.ToUnixTimestamp();
        }

        public static DateTime FromUnixTimestamp(this int unixTimeStamp)
            => epoch.AddSeconds(unixTimeStamp);

        public static DateTime? FromUnixTimestamp(this int? unixTimeStamp)
        {
            if (unixTimeStamp == null)
                return null;

            return unixTimeStamp.Value.FromUnixTimestamp();
        }
    }
}
