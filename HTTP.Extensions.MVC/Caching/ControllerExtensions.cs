using HTTP.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HTTP.Extensions.MVC.Caching
{
    public static class ControllerExtensions
    {
        public static ActionResult IfModifiedSince(this Controller controller, Lazy<DateTime> lastModified, Lazy<ActionResult> decoratedResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");

            return new IfModifiedSinceResult(lastModified, decoratedResult);
        }

        public static ActionResult IfUnmodifiedSince(this Controller controller, Lazy<DateTime> lastModified, Lazy<ActionResult> decoratedResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");

            return new IfUnmodifiedSinceResult(lastModified, decoratedResult);
        }

        public static ActionResult IfMatch(this Controller controller, Func<EntityTagCondition, bool> etagValidator, Lazy<EntityTag> currentETag, Lazy<ActionResult> decoratedResult)
        {
            if (etagValidator == null) throw new ArgumentNullException("etagValidator");
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");

            return new IfMatchResult(etagValidator, currentETag, decoratedResult);
        }

        public static ActionResult IfMatch(this Controller controller, IEnumerable<EntityTag> validETags, Lazy<EntityTag> currentETag, Lazy<ActionResult> decoratedResult)
        {
            if (validETags == null) throw new ArgumentNullException("validETags");
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");

            return new IfMatchResult(condition => condition.IsValid(validETags), currentETag, decoratedResult);
        }

        public static ActionResult IfMatch(this Controller controller, Lazy<EntityTag> currentETag, Lazy<ActionResult> decoratedResult)
        {
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");

            return new IfMatchResult(condition => condition.IsValid(currentETag.Value), currentETag, decoratedResult);
        }
    }
}
