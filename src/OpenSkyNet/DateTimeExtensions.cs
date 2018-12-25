using System;

namespace OpenSkyNet
{
    static class DateTimeExtensions
    {
        static readonly DateTime epoch = new DateTime(1970, 1, 1);

        public static int ToUnixTimestamp(this DateTime time)
        {
            var result = (int)time.ToUniversalTime().Subtract(epoch).TotalSeconds;
            if (result < 0)
                return -1;

            return result;
        }
    }
}
