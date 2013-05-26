using HTTP.Extensions.Caching;
using HTTP.Extensions.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTP.Extensions
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
			var value = request.Headers[ExpirationHeaders.EXPIRES];
			if (value == null) return null;

			return new Tokenizer(value).TryReadDateTime();
        }

        public static DateTime? GetLastModified(this HttpWebResponse request)
        {
			var value = request.Headers[ExpirationHeaders.LAST_MODIFIED];
			if (value == null) return null;

			return new Tokenizer(value).TryReadDateTime();
        }

        public static void SetIfModifiedSince(this HttpWebRequest response, DateTime ifModifiedSince)
        {
			response.Headers[ExpirationHeaders.IF_MODIFIED_SINCE] = ifModifiedSince.AsHttpDateTime();
        }

        public static void SetIfUnmodifiedSince(this HttpWebRequest response, DateTime ifUnmodifiedSince)
        {
			response.Headers[ExpirationHeaders.IF_UNMODIFIED_SINCE] = ifUnmodifiedSince.AsHttpDateTime();
        }
    }
}
