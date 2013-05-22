using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTP.Extensions
{
    internal static class HeaderClientExtensions
    {
        public static DateTime? GetDateTimeHeader(this HttpWebResponse response, string header)
        {
            var value = response.Headers[header];
            if (value == null) return null;

            return value.AsHttpDateTime();
        }

        public static void SetDateTimeHeader(this HttpWebRequest request, string header, DateTime value)
        {
            request.Headers[header] = value.AsHttpDateTime();
        }
    }
}
