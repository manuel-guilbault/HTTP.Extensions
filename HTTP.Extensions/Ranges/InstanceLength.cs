using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class InstanceLength
    {
        public static readonly InstanceLength Unknown = new UnknownInstanceLength();

        private long value;

        internal InstanceLength()
        {
            value = -1;
        }

        public InstanceLength(long value)
        {
            if (value < 0) throw new ArgumentException("value must be equal to or greater than zero", "value");

            this.value = value;
        }

        public long Value
        {
            get { return value; }
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    internal class UnknownInstanceLength : InstanceLength
    {
        public override string ToString()
        {
            return "*";
        }
    }
}
