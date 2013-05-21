using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTTP.Extensions.Caching;
using System.Web.Mvc;

namespace HTTP.Extensions.MVC.Caching
{
    public class IfMatchResult : ActionResult
    {
        private Lazy<ActionResult> decoratedResult;
        private Func<EntityTag[], bool> etagValidator;

        public IfMatchResult(Lazy<ActionResult> decoratedResult, Func<EntityTag[], bool> etagValidator)
        {
            if (decoratedResult == null) throw new ArgumentNullException("decoratedResult");
            if (etagValidator == null) throw new ArgumentNullException("etagValidator");

            this.decoratedResult = decoratedResult;
            this.etagValidator = etagValidator;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var ifMatch = context.HttpContext.Request.GetIfMatch();
            if (ifMatch != null && ifMatch.Any() && etagValidator(ifMatch.ToArray()))
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
