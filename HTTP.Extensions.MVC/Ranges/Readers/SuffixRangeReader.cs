using HTTP.Extensions.Ranges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.MVC.Ranges.Readers
{
    public class SuffixRangeReader : IRangeReader
    {
        public RangeStream ReadRange(ISubRange range, Stream stream)
        {
            if (range == null) throw new ArgumentNullException("range");
            if (stream == null) throw new ArgumentNullException("stream");

            var suffixRange = (SuffixSubRange)range;
            return new RangeStream(
                Math.Max(0, stream.Length - suffixRange.MaxLength),
                stream.Length - 1,
                stream
            );
        }
    }
}
