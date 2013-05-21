using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace HTTP.Extensions.Caching
{
    public static class ExpirationServerExtensions
    {
        public static void SetAge(this HttpResponseBase response, TimeSpan age)
        {
            response.Headers[ExpirationHeaders.AGE] = age.TotalSeconds.ToString();
        }

        public static void SetExpires(this HttpResponseBase response, DateTime expires)
        {
            response.SetDateTimeHeader(ExpirationHeaders.EXPIRES, expires);
        }

        public static void SetExpires(this HttpResponseBase response, TimeSpan expires)
        {
            response.SetExpires(DateTime.UtcNow.Add(expires));
        }

        public static void SetLastModified(this HttpResponseBase response, DateTime lastModified)
        {
            response.SetDateTimeHeader(ExpirationHeaders.LAST_MODIFIED, lastModified);
        }

        public static DateTime? GetIfModifiedSince(this HttpRequestBase request)
        {
            return request.GetDateTimeHeader(ExpirationHeaders.IF_MODIFIED_SINCE);
        }

        public static DateTime? GetIfUnmodifiedSince(this HttpRequestBase request)
        {
            return request.GetDateTimeHeader(ExpirationHeaders.IF_UNMODIFIED_SINCE);
        }
    }
}
