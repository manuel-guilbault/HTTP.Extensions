using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    internal static class RangeHeaders
    {
        public const string RANGE = "Range";
        public const string ACCEPT_RANGES = "Accept-Ranges";
        public const string CONTENT_RANGE = "Content-Range";
        public const string IF_RANGE = "If-Range";
    }
}
