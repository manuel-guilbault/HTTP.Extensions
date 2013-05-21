using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Caching
{
    internal static class ExpirationHeaders
    {
        public const string AGE = "Age";
        public const string EXPIRES = "Expires";
        public const string LAST_MODIFIED = "Last-Modified";
        public const string IF_MODIFIED_SINCE = "If-Modified-Since";
        public const string IF_UNMODIFIED_SINCE = "If-Unmodified-Since";
    }
}
