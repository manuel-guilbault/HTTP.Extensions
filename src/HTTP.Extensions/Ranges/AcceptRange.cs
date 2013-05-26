using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class AcceptRange
    {
        public static readonly AcceptRange None = new AcceptRange();

        private RangeUnit[] units;

        internal AcceptRange()
        {
        }

        public AcceptRange(params RangeUnit[] units)
        {
            if (units == null) throw new ArgumentNullException("units");
            if (!units.Any()) throw new ArgumentException("units cannot be empty", "units");
            if (units.Any(u => u == null)) throw new ArgumentException("units cannot contain null elements", "units");

            this.units = units.ToArray();
        }

        public RangeUnit[] Units
        {
            get { return units; }
        }

        public override string ToString()
        {
            if (units == null)
            {
                return "none";
            }

            return string.Join(", ", units.Select(unit => unit.Name).ToArray());
        }
    }
}
