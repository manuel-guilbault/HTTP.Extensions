using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class RangeUnitRegistry : IEnumerable<RangeUnit>
    {
        public static readonly RangeUnit Bytes = new RangeUnit("bytes");

        public static readonly RangeUnitRegistry Default = new RangeUnitRegistry()
        {
            Bytes
        };

        private Dictionary<string, RangeUnit> units = new Dictionary<string, RangeUnit>();

        public RangeUnitRegistry(params RangeUnit[] units)
            : this(units.AsEnumerable())
        {
        }

        public RangeUnitRegistry(IEnumerable<RangeUnit> units)
        {
            if (units == null) throw new ArgumentNullException("units");

            foreach (var unit in units)
            {
                Add(unit);
            }
        }

        public void Add(RangeUnit unit)
        {
            if (unit == null) throw new ArgumentNullException("unit");
            if (units.ContainsKey(unit.Name)) throw new ArgumentException(string.Format("unit named '{0}' already exists", unit.Name));

            units.Add(unit.Name, unit);
        }

        public RangeUnit Get(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            RangeUnit unit;
            units.TryGetValue(name, out unit);
            return unit;
        }

        public RangeUnitRegistry Merge(RangeUnitRegistry registry)
        {
            if (registry == null) throw new ArgumentNullException("registry");

            var mergedUnits = this.Concat(registry);
            if (mergedUnits.GroupBy(unit => unit.Name).Any(group => group.Count() > 1))
            {
                throw new ArgumentException("result contains duplicates");
            }

            return new RangeUnitRegistry(mergedUnits);
        }

        public IEnumerator<RangeUnit> GetEnumerator()
        {
            return units.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
