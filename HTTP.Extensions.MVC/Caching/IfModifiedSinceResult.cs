using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using HTTP.Extensions.Caching;

namespace HTTP.Extensions.MVC.Caching
{
    public class IfModifiedSinceResult : ActionResult
    {
        private Lazy<DateTime> lastModified;
        private Lazy<ActionResult> decoratedResult;

        public IfModifiedSinceResult(Lazy<DateTime> lastModified, Lazy<ActionResult> decoratedResult)
        {
            if (lastModified == null) throw new ArgumentNullException("lastModified");
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");

            this.lastModified = lastModified;
            this.decoratedResult = decoratedResult;
        }

        protected virtual void ExecuteResultWhenModified(ControllerContext context)
        {
            context.HttpContext.Response.SetLastModified(lastModified.Value);
            decoratedResult.Value.ExecuteResult(context);
        }

        protected virtual void ExecuteResultWhenUnmodified(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 304; // Not Modified
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var ifModifiedSince = context.HttpContext.Request.GetIfModifiedSince();
            if (ifModifiedSince == null || lastModified.Value > ifModifiedSince)
            {
                ExecuteResultWhenModified(context);
            }
            else
            {
                ExecuteResultWhenUnmodified(context);
            }
        }
    }
}
