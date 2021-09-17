using System;

namespace TrackingRemoteHostService.Extensions
{
    public static class TimeExtensions
    {
        public static long GetUnixTime(this DateTime winTime)
        {
            return new DateTimeOffset(winTime).ToUnixTimeMilliseconds();
        }

        public static DateTime GetNormalTime(this long unixDate)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddSeconds(unixDate).ToLocalTime();
            return date;
        }

        public static DateTime GetNormalTime(this byte[] unixDate)
        {
            long longVar = BitConverter.ToInt64(unixDate, 0);
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddSeconds(longVar).ToLocalTime();
            return date;
        }
    }
}
