using HTTP.Extensions.Parsing;
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
			if (dateTime.Kind == DateTimeKind.Local)
			{
				dateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime);
			}
            return dateTime.ToString("r", CultureInfo.InvariantCulture);
        }
    }
}
