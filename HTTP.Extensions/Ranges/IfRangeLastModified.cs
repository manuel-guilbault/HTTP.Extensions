using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class IfRangeLastModified : IfRange
    {
        private DateTime value;

        public IfRangeLastModified(DateTime value)
        {
            this.value = value;
        }

        public DateTime Value
        {
            get { return value; }
        }

        public override string ToString()
        {
            return value.AsHttpDateTime();
        }
    }
}
