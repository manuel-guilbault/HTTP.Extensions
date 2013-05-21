using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTP.Extensions.Caching
{
    public static class ExpirationClientExtensions
    {
        public static TimeSpan? GetAge(this HttpWebResponse request)
        {
            var value = request.Headers[ExpirationHeaders.AGE];
            if (value == null) return null;

            int seconds;
            if (!int.TryParse(value, out seconds)) return null;

            return TimeSpan.FromSeconds(seconds);
        }

        public static DateTime? GetExpires(this HttpWebResponse request)
        {
            return request.GetDateTimeHeader(ExpirationHeaders.EXPIRES);
        }

        public static DateTime? GetLastModified(this HttpWebResponse request)
        {
            return request.GetDateTimeHeader(ExpirationHeaders.LAST_MODIFIED);
        }

        public static void SetIfModifiedSince(this HttpWebRequest response, DateTime ifModifiedSince)
        {
            response.SetDateTimeHeader(ExpirationHeaders.IF_MODIFIED_SINCE, ifModifiedSince);
        }

        public static void SetIfUnmodifiedSince(this HttpWebRequest response, DateTime ifUnmodifiedSince)
        {
            response.SetDateTimeHeader(ExpirationHeaders.IF_UNMODIFIED_SINCE, ifUnmodifiedSince);
        }
    }
}
