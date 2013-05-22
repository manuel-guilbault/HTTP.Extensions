using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class ContentRange
    {
        private RangeUnit unit;
        private ContentSubRange range;
        private InstanceLength instanceLength;

        public ContentRange(RangeUnit unit, ContentSubRange range, InstanceLength instanceLength)
        {
            if (unit == null) throw new ArgumentNullException("unit");
            if (range == null) throw new ArgumentNullException("range");
            if (instanceLength == null) throw new ArgumentNullException("instanceLength");

            this.unit = unit;
            this.range = range;
            this.instanceLength = instanceLength;
        }

        public RangeUnit Unit
        {
            get { return unit; }
        }

        public ContentSubRange Range
        {
            get { return range; }
        }

        public InstanceLength InstanceLength
        {
            get { return instanceLength; }
        }

        public override string ToString()
        {
            return unit.Name + " " + range.ToString() + "/" + instanceLength.ToString();
        }
    }
}
