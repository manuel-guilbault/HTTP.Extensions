using HTTP.Extensions.Caching;
using HTTP.Extensions.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HTTP.Extensions.Ranges
{
    public static class ServerExtensions
    {
        public static Range GetRange(this HttpRequestBase request)
        {
            return request.GetRange(RangeUnitRegistry.Default);
        }

        public static Range GetRange(this HttpRequestBase request, RangeUnitRegistry units)
        {
            if (units == null) throw new ArgumentNullException("units");

            var value = request.Headers[RangeHeaders.RANGE];
            if (value == null) return null;

            try
            {
                return new HeaderReader<Range>(new RangeParser(RangeUnitRegistry.Default)).Read(value);
            }
            catch (ParsingException)
            {
                return null;
            }
        }

        public static void SetAcceptRange(this HttpResponseBase response, AcceptRange acceptRange)
        {
            if (acceptRange == null) throw new ArgumentNullException("acceptRange");

            response.Headers[RangeHeaders.ACCEPT_RANGES] = acceptRange.ToString();
        }

        public static void SetContentRange(this HttpResponseBase response, ContentRange contentRange)
        {
            if (contentRange == null) throw new ArgumentNullException("contentRange");

            response.Headers[RangeHeaders.CONTENT_RANGE] = contentRange.ToString();
        }

        public static IfRange GetIfRange(this HttpRequestBase request)
        {
            var value = request.Headers[RangeHeaders.IF_RANGE];
            if (value == null) return null;

            try
            {
                return new HeaderReader<IfRange>(new IfRangeParser(new EntityTagParser())).Read(value);
            }
            catch (ParsingException)
            {
                return null;
            }
        }
    }
}
