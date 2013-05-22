using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using HTTP.Extensions.Caching;

namespace HTTP.Extensions.MVC.Caching
{
    public class IfUnmodifiedSinceResult : ActionResult
    {
        private Lazy<DateTime> lastModified;
        private Lazy<ActionResult> decoratedResult;

        public IfUnmodifiedSinceResult(Lazy<DateTime> lastModified, Lazy<ActionResult> decoratedResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");

            this.lastModified = lastModified;
            this.decoratedResult = decoratedResult;
        }

        protected virtual void ExecuteResultWhenUnmodified(ControllerContext context)
        {
            decoratedResult.Value.ExecuteResult(context);
        }

        protected virtual void ExecuteResultWhenModified(ControllerContext context)
        {
            context.HttpContext.Response.SetLastModified(lastModified.Value);
            context.HttpContext.Response.StatusCode = 412; // Precondition Failed
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var ifUnmodifiedSince = context.HttpContext.Request.GetIfUnmodifiedSince();
            if (ifUnmodifiedSince == null || lastModified.Value <= ifUnmodifiedSince)
            {
                ExecuteResultWhenUnmodified(context);
            }
            else
            {
                ExecuteResultWhenModified(context);
            }
        }
    }
}
