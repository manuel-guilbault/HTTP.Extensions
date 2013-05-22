using HTTP.Extensions.Ranges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.MVC.Ranges.Readers
{
    public class ClosedRangeReader : IRangeReader
    {
        public RangeStream ReadRange(ISubRange range, Stream stream)
        {
            if (range == null) throw new ArgumentNullException("range");
            if (stream == null) throw new ArgumentNullException("stream");

            var closedRange = (ClosedSubRange)range;
            return new RangeStream(
                closedRange.StartAt,
                closedRange.EndAt,
                stream
            );
        }
    }
}
