using HTTP.Extensions.Ranges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.MVC.Ranges.Readers
{
    public class DelegateRangeReader : IRangeReader
    {
        private IRangeReaderResolver resolver;

        public DelegateRangeReader()
            : this(new RangeReaderResolver())
        {
        }

        public DelegateRangeReader(IRangeReaderResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException("resolver");

            this.resolver = resolver;
        }

        public RangeStream ReadRange(ISubRange range, Stream stream)
        {
            if (range == null) throw new ArgumentNullException("range");
            if (stream == null) throw new ArgumentNullException("stream");

            var reader = resolver.Resolve(range);
            return reader.ReadRange(range, stream);
        }
    }
}
