using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class Range
    {
        private readonly RangeUnit unit;
        private readonly SubRange[] ranges;

		public Range(RangeUnit unit, params SubRange[] ranges)
        {
            if (unit == null) throw new ArgumentNullException("unit");
            if (ranges == null) throw new ArgumentNullException("ranges");
            if (!ranges.Any()) throw new ArgumentException("ranges cannot be empty", "ranges");
            if (ranges.Any(r => r == null)) throw new ArgumentException("ranges cannot contain null elements", "ranges");

            this.unit = unit;
            this.ranges = ranges.ToArray();
        }

        public RangeUnit Unit
        {
            get { return unit; }
        }

		public SubRange[] Ranges
        {
            get { return ranges; }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(unit.ToString());
            builder.Append("=");
            for (int i = 0; i < ranges.Length; ++i)
            {
                if (i > 0)
                {
                    builder.Append(", ");
                }
                builder.Append(ranges[i].ToString());
            }
            return builder.ToString();
        }
    }
}
