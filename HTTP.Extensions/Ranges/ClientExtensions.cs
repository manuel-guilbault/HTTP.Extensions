using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public static class ClientExtensions
    {
        public static void SetRange(this HttpWebRequest request, Range range)
        {
            if (range == null) throw new ArgumentNullException("range");

            request.Headers[RangeHeaders.RANGE] = range.ToString();
        }

        public static AcceptRange GetAcceptRange(this HttpWebResponse response)
        {
            var value = response.Headers[RangeHeaders.ACCEPT_RANGES];
            if (value == null) return null;

            try
            {
                return new AcceptRangeParser().Parse(value);
            }
            catch (ParserException)
            {
                return null;
            }
        }

        public static ContentRange GetContentRange(this HttpWebResponse response)
        {
            var value = response.Headers[RangeHeaders.ACCEPT_RANGES];
            if (value == null) return null;

            try
            {
                return new ContentRangeParser().Parse(value);
            }
            catch (ParserException)
            {
                return null;
            }
        }

        public static void SetIfRange(this HttpWebRequest request, IfRange ifRange)
        {
            if (ifRange == null) throw new ArgumentNullException("ifRange");

            request.Headers[RangeHeaders.IF_RANGE] = ifRange.ToString();
        }
    }
}
