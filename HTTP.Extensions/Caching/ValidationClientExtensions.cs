using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTP.Extensions.Caching
{
    public static class ValidationClientExtensions
    {
        public static EntityTag GetETag(this HttpWebResponse response)
        {
            var tags = response.GetEntityTags(ValidationHeaders.E_TAG);
            return tags.Count() > 1 ? null : tags.SingleOrDefault();
        }

        public static void SetIfMatch(this HttpWebRequest request, EntityTag ifMatch)
        {
            if (ifMatch == null) throw new ArgumentNullException("ifMatch");

            request.Headers[ValidationHeaders.IF_MATCH] = ifMatch.ToString();
        }

        public static void SetIfNoneMatch(this HttpWebRequest request, EntityTag ifNoneMatch)
        {
            if (ifNoneMatch == null) throw new ArgumentNullException("ifNoneMatch");

            request.Headers[ValidationHeaders.IF_NONE_MATCH] = ifNoneMatch.ToString();
        }
    }
}
