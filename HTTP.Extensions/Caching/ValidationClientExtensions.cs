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
            return response.GetEntityTag(ValidationHeaders.E_TAG);
        }

        public static void SetIfMatch(this HttpWebRequest request, EntityTagCondition ifMatch)
        {
            if (ifMatch == null) throw new ArgumentNullException("ifMatch");

            request.Headers[ValidationHeaders.IF_MATCH] = ifMatch.ToString();
        }

        public static void SetIfNoneMatch(this HttpWebRequest request, EntityTagCondition ifNoneMatch)
        {
            if (ifNoneMatch == null) throw new ArgumentNullException("ifNoneMatch");

            request.Headers[ValidationHeaders.IF_NONE_MATCH] = ifNoneMatch.ToString();
        }
    }
}
