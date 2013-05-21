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
        private Lazy<ActionResult> decoratedResult;
        private Lazy<DateTime> lastModified;

        public IfUnmodifiedSinceResult(Lazy<ActionResult> decoratedResult, Lazy<DateTime> lastModified)
        {
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");
            if (lastModified == null) throw new ArgumentNullException("lastModified");

            this.decoratedResult = decoratedResult;
            this.lastModified = lastModified;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var ifUnmodifiedSince = context.HttpContext.Request.GetIfUnmodifiedSince();
            if (ifUnmodifiedSince != null && ifUnmodifiedSince > lastModified.Value)
            {
                context.HttpContext.Response.StatusCode = 412; // Precondition Failed
            }
            else
            {
                decoratedResult.Value.ExecuteResult(context);
            }
        }
    }
}
