using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace HTTP.Extensions
{
    internal static class HeaderServerExtensions
    {
        public static DateTime? GetDateTimeHeader(this HttpRequestBase request, string header)
        {
            var value = request.Headers[header];
            if (value == null) return null;

            return value.AsHttpDateTime();
        }

        public static IEnumerable<DateTime> GetDateTimeHeaders(this HttpRequestBase request, string header)
        {
            return request.Headers.GetValues(header).Select(value => value.AsHttpDateTime()).Where(value => value != null).Select(value => value.Value);
        }

        public static void SetDateTimeHeader(this HttpResponseBase response, string header, DateTime value)
        {
            response.Headers[header] = value.AsHttpDateTime();
        }
    }
}
