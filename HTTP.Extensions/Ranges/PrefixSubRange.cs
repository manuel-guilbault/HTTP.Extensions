using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class PrefixSubRange : ISubRange
    {
        private readonly long startAt;

        public PrefixSubRange(long startAt)
        {
            if (startAt < 0) throw new ArgumentException("startAt must be equal to or greater than zero", "startAt");

            this.startAt = startAt;
        }

        public long StartAt
        {
            get { return startAt; }
        }

        public override string ToString()
        {
            return startAt + "-";
        }
    }
}
