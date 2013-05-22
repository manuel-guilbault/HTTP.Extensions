using HTTP.Extensions.MVC.Ranges.Readers;
using HTTP.Extensions.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HTTP.Extensions.MVC.Ranges
{
    public abstract class RangeResult : ActionResult
    {
        private string contentType;
        private RangeUnitRegistry acceptedUnits;

        public RangeResult(string contentType, RangeUnitRegistry acceptedUnits)
        {
            if (contentType == null) throw new ArgumentNullException("contentType");
            if (acceptedUnits == null) throw new ArgumentNullException("acceptedUnits");

            this.contentType = contentType;
            this.acceptedUnits = acceptedUnits;
        }

        public string ContentType
        {
            get { return contentType; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                contentType = value;
            }
        }

        public RangeUnitRegistry AcceptedUnits
        {
            get { return acceptedUnits; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");

                acceptedUnits = value;
            }
        }

        protected void PrepareResponse(ControllerContext context, int status)
        {
            context.HttpContext.Response.StatusCode = status;
            context.HttpContext.Response.ContentType = contentType;
            context.HttpContext.Response.SetAcceptRange(new AcceptRange(acceptedUnits.ToArray()));
        }

        protected virtual void SendAll(ControllerContext context)
        {
            PrepareResponse(context, 200); //Ok
            SendAllContent(context);
        }

        protected abstract void SendAllContent(ControllerContext context);

        protected virtual void TrySendRange(ControllerContext context, RangeUnit unit, ISubRange range)
        {
            try
            {
                SendRange(context, unit, range);
            }
            catch (IndexOutOfRangeException)
            {
                SendRangeNotSatisfiable(context);
            }
        }

        protected virtual void SendRange(ControllerContext context, RangeUnit unit, ISubRange range)
        {
            var rangeStream = GetRangeStream(context, unit, range);

            PrepareResponse(context, 206); //Partial Content
            context.HttpContext.Response.SetContentRange(
                new ContentRange(
                    unit,
                    new ContentSubRange(rangeStream.StartAt, rangeStream.EndAt),
                    new InstanceLength(rangeStream.TotalLength)
                )
            );
            rangeStream.CopyTo(context.HttpContext.Response.OutputStream);
        }

        protected abstract RangeStream GetRangeStream(ControllerContext context, RangeUnit unit, ISubRange range);

        protected virtual void SendRangeNotSatisfiable(ControllerContext context)
        {
            PrepareResponse(context, 416); //Requested range not satisfiable
        }

        protected virtual void TrySendRanges(ControllerContext context, RangeUnit unit, ISubRange[] ranges)
        {
            throw new NotImplementedException();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var range = context.HttpContext.Request.GetRange();
            if (range == null)
            {
                SendAll(context);
            }
            else if (range.Ranges.Length == 1)
            {
                TrySendRange(context, range.Unit, range.Ranges.First());
            }
            else
            {
                TrySendRanges(context, range.Unit, range.Ranges);
            }
        }
    }
}
