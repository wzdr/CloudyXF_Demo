using System;
using System.Collections.Generic;
using System.Text;

namespace CloudyXF.Helpers
{
    public static class Conversions
    {
        public static double ToCelcius(double value)
        {
            return (value - 32.0) / 1.8;
        }

        public static double ToKmPerHour(double value)
        {
            return value * 1.609344;
        }

        public static DateTime ConvertFromUnixTimeToLocalTime(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var localTime = origin.AddSeconds(timestamp).ToLocalTime();
            return localTime;
        }

    }
}
