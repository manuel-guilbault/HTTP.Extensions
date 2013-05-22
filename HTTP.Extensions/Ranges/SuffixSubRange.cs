using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class SuffixSubRange : ISubRange
    {
        private readonly long maxLength;

        public SuffixSubRange(long maxLength)
        {
            if (maxLength < 0) throw new ArgumentException("maxLength must be equal to or greater than zero", "maxLength");

            this.maxLength = maxLength;
        }

        public long MaxLength
        {
            get { return maxLength; }
        }

        public override string ToString()
        {
            return "-" + maxLength;
        }
    }
}
