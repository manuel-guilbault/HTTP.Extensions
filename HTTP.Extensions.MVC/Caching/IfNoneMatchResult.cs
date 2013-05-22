using HTTP.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HTTP.Extensions.MVC.Caching
{
    public class IfNoneMatchResult : ActionResult
    {
        private Func<EntityTagCondition, bool> etagValidator;
        private Lazy<EntityTag> currentETag;
        private Lazy<ActionResult> decoratedResult;

        public IfNoneMatchResult(Func<EntityTagCondition, bool> etagValidator, Lazy<EntityTag> currentETag, Lazy<ActionResult> decoratedResult)
        {
            if (etagValidator == null) throw new ArgumentNullException("etagValidator");
            if (currentETag == null) throw new ArgumentNullException("currentETag");
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");

            this.etagValidator = etagValidator;
            this.currentETag = currentETag;
            this.decoratedResult = decoratedResult;
        }

        protected virtual void ExecuteResultWhenNoMatch(ControllerContext context)
        {
            context.HttpContext.Response.SetETag(currentETag.Value);
            decoratedResult.Value.ExecuteResult(context);
        }

        protected virtual void ExecuteResultWhenMatch(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 412; // Precondition Failed
            context.HttpContext.Response.SetETag(currentETag.Value);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var ifNoneMatch = context.HttpContext.Request.GetIfNoneMatch();
            if (ifNoneMatch == null || !etagValidator(ifNoneMatch))
            {
                ExecuteResultWhenNoMatch(context);
            }
            else
            {
                ExecuteResultWhenMatch(context);
            }
        }
    }
}
