using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using HTTP.Extensions.Ranges;
using System.IO;
using HTTP.Extensions.MVC.Ranges.Readers;

namespace HTTP.Extensions.MVC.Ranges
{
    public class StreamRangeResult : RangeResult, IDisposable
    {
        private Stream stream;
        private IRangeReader rangeReader;

        private bool isDisposed = false;

        public StreamRangeResult(Stream stream, string contentType)
            : this(stream, contentType, new DelegateRangeReader())
        {
        }

        public StreamRangeResult(Stream stream, string contentType, IRangeReader rangeReader)
            : base(contentType, new RangeUnitRegistry() { RangeUnitRegistry.Bytes })
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (rangeReader == null) throw new ArgumentNullException("rangeReader");

            this.stream = stream;
            this.rangeReader = rangeReader;
        }

        protected override void SendAllContent(ControllerContext context)
        {
            stream.CopyTo(context.HttpContext.Response.OutputStream);
        }

        protected override RangeStream GetRangeStream(ControllerContext context, RangeUnit unit, ISubRange range)
        {
            return rangeReader.ReadRange(range, stream);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            try
            {
                base.ExecuteResult(context);
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                stream.Dispose();
                isDisposed = true;
            }
        }
    }
}
