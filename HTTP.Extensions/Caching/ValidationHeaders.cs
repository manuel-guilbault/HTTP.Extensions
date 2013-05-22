using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace HTTP.Extensions.Caching
{
    internal static class ValidationHeaders
    {
        public const string E_TAG = "ETag";
        public const string IF_MATCH = "If-Match";
        public const string IF_NONE_MATCH = "If-None-Match";

        public static EntityTag GetEntityTag(this HttpRequestBase request, string header)
        {
            var value = request.Headers[header];
            if (value == null) return null;

            try
            {
                return new EntityTagParser().Parse(value);
            }
            catch (ParserException)
            {
                return null;
            }
        }

        public static EntityTag GetEntityTag(this HttpWebResponse response, string header)
        {
            var value = response.Headers[header];
            if (value == null) return null;

            try
            {
                return new EntityTagParser().Parse(value);
            }
            catch (ParserException)
            {
                return null;
            }
        }

        public static EntityTagCondition GetEntityTagCondition(this HttpRequestBase request, string header)
        {
            var value = request.Headers[header];
            if (value == null) return null;

            try
            {
                return new EntityTagConditionParser().Parse(value);
            }
            catch (ParserException)
            {
                return null;
            }
        }

        public static EntityTagCondition GetEntityTagCondition(this HttpWebResponse response, string header)
        {
            var value = response.Headers[header];
            if (value == null) return null;

            try
            {
                return new EntityTagConditionParser().Parse(value);
            }
            catch (ParserException)
            {
                return null;
            }
        }
    }
}
