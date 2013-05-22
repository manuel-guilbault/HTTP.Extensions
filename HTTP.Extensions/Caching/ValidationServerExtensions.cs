using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HTTP.Extensions.Caching
{
    public static class ValidationServerExtensions
    {
        public static void SetETag(this HttpResponseBase response, EntityTag etag)
        {
            if (etag == null) throw new ArgumentNullException("etag");

            response.Headers[ValidationHeaders.E_TAG] = etag.ToString();
        }

        public static EntityTagCondition GetIfMatch(this HttpRequestBase request)
        {
            return request.GetEntityTagCondition(ValidationHeaders.IF_MATCH);
        }

        public static EntityTagCondition GetIfNoneMatch(this HttpRequestBase request)
        {
            return request.GetEntityTagCondition(ValidationHeaders.IF_NONE_MATCH);
        }
    }
}
