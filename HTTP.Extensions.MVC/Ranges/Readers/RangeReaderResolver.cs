using HTTP.Extensions.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.MVC.Ranges.Readers
{
    public class RangeReaderResolver : IRangeReaderResolver
    {
        private Dictionary<Type, IRangeReader> readers = new Dictionary<Type, IRangeReader>()
        {
            { typeof(PrefixSubRange), new PrefixRangeReader() },
            { typeof(ClosedSubRange), new ClosedRangeReader() },
            { typeof(SuffixSubRange), new SuffixRangeReader() }
        };

        public IRangeReader Resolve(ISubRange range)
        {
            if (range == null) throw new ArgumentNullException("range");

            IRangeReader reader;
            readers.TryGetValue(range.GetType(), out reader);
            if (reader == null) throw new ArgumentException(string.Format("No reader for type {0}.", range.GetType()), "range");

            return reader;
        }
    }
}
