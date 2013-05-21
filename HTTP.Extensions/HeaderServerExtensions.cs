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

            DateTime dateTime;
            if (!DateTime.TryParseExact(value, "r", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime))
            {
                return null;
            }

            return dateTime;
        }

        public static IEnumerable<DateTime> GetDateTimeHeaders(this HttpRequestBase request, string header)
        {
            return request.Headers.GetValues(header).Select(value =>
            {
                DateTime dateTime;
                return DateTime.TryParseExact(value, "r", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime)
                    ? dateTime
                    : (DateTime?)null;
            }).Where(value => value != null).Select(value => value.Value);
        }

        public static void SetDateTimeHeader(this HttpResponseBase response, string header, DateTime value)
        {
            response.Headers[header] = value.ToString("r", CultureInfo.InvariantCulture);
        }

        public static void AddDateTimeHeader(this HttpResponseBase response, string header, DateTime value)
        {
            response.Headers.Add(header, value.ToString("r", CultureInfo.InvariantCulture));
        }
    }
}
