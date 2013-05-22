using HTTP.Extensions.Ranges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.MVC.Ranges.Readers
{
    public class PrefixRangeReader : IRangeReader
    {
        public RangeStream ReadRange(ISubRange range, Stream stream)
        {
            if (range == null) throw new ArgumentNullException("range");
            if (stream == null) throw new ArgumentNullException("stream");

            var prefixRange = (PrefixSubRange)range;
            return new RangeStream(
                prefixRange.StartAt,
                stream.Length - 1,
                stream
            );
        }
    }
}
