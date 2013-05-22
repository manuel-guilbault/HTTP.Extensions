using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HTTP.Extensions
{
    public static class DateTimeExtensions
    {
        public static string AsHttpDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("r", CultureInfo.InvariantCulture);
        }

        public static DateTime? AsHttpDateTime(this string value)
        {
            DateTime dateTime;
            return DateTime.TryParseExact(value, "r", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime)
                ? dateTime
                : (DateTime?)null;
        }
    }
}
