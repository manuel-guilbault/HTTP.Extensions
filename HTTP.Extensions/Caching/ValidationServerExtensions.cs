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

        public static IEnumerable<EntityTag> GetIfMatch(this HttpRequestBase request)
        {
            return request.GetEntityTags(ValidationHeaders.IF_MATCH);
        }

        public static IEnumerable<EntityTag> GetIfNoneMatch(this HttpRequestBase request)
        {
            return request.GetEntityTags(ValidationHeaders.IF_NONE_MATCH);
        }
    }
}
