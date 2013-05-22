using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class AcceptRangeParser : ParserBase, IHeaderParser<AcceptRange>
    {
        private const string NONE = "none";
        private const string COMMA = ",";

        private RangeUnitRegistry units;

        public AcceptRangeParser()
            : this(RangeUnitRegistry.Default)
        {
        }

        public AcceptRangeParser(RangeUnitRegistry units)
        {
            if (units == null) throw new ArgumentNullException("units");

            this.units = units;
        }

        public AcceptRange Parse(string value)
        {
            try
            {
                Initialize(value);

                if (value.Trim() == NONE)
                {
                    return AcceptRange.None;
                }

                return new AcceptRange(ParseUnits().ToArray());
            }
            catch (ParserException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ParserException();
            }
        }

        protected IEnumerable<RangeUnit> ParseUnits()
        {
            yield return ParseUnit();
            SkipWhiteSpaces();

            while (!IsAtEnd())
            {
                yield return ParseUnit();
                SkipWhiteSpaces();
            }
        }

        protected RangeUnit ParseUnit()
        {
            var name = ReadUntil(COMMA);
            var unit = units.Get(name);
            if (unit == null) throw new ParserException();

            return unit;
        }
    }
}
