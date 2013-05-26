using HTTP.Extensions.Caching;
using HTTP.Extensions.Parsing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace HTTP.Extensions
{
    public static class ExpirationServerExtensions
    {
        public static void SetAge(this HttpResponseBase response, TimeSpan age)
        {
            response.Headers[ExpirationHeaders.AGE] = age.TotalSeconds.ToString();
        }

        public static void SetExpires(this HttpResponseBase response, DateTime expires)
        {
            response.Headers[ExpirationHeaders.EXPIRES] = expires.AsHttpDateTime();
        }

        public static void SetExpires(this HttpResponseBase response, TimeSpan expires)
        {
            response.SetExpires(DateTime.UtcNow.Add(expires));
        }

        public static void SetLastModified(this HttpResponseBase response, DateTime lastModified)
        {
			response.Headers[ExpirationHeaders.LAST_MODIFIED] = lastModified.AsHttpDateTime();
        }

        public static DateTime? GetIfModifiedSince(this HttpRequestBase request)
        {
			var value = request.Headers[ExpirationHeaders.IF_MODIFIED_SINCE];
			if (value == null) return null;

			return new Tokenizer(value).TryReadDateTime();
        }

        public static DateTime? GetIfUnmodifiedSince(this HttpRequestBase request)
        {
			var value = request.Headers[ExpirationHeaders.IF_UNMODIFIED_SINCE];
			if (value == null) return null;

			return new Tokenizer(value).TryReadDateTime();
        }
    }
}
