using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class RangeParser : ParserBase, IHeaderParser<Range>
    {
        private const string EQUAL = "=";
        private const string COMMA = ",";
        private const string DASH = "-";

        private RangeUnitRegistry units;

        public RangeParser(RangeUnitRegistry units)
        {
            if (units == null) throw new ArgumentNullException("units");

            this.units = units;
        }

        public Range Parse(string value)
        {
            try
            {
                Initialize(value);

                SkipWhiteSpaces();
                var unit = ParseUnit();

                SkipWhiteSpaces();
                Read(EQUAL);

                var ranges = ParseRanges().ToArray();

                return new Range(unit, ranges);
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

        protected RangeUnit ParseUnit()
        {
            var unitName = ReadUntil(EQUAL);

            var unit = units.Get(unitName);
            if (unit == null) throw new ParserException();

            return unit;
        }

        protected IEnumerable<ISubRange> ParseRanges()
        {
            yield return ParseRange();

            while (!IsAtEnd())
            {
                SkipWhiteSpaces();
                Read(COMMA);

                SkipWhiteSpaces();
                yield return ParseRange();
            }
        }

        protected ISubRange ParseRange()
        {
            if (Peek(DASH))
            {
                return ParseSuffixRange();
            }
            
            var start = ReadWhile(char.IsDigit);
            if (start == "") throw new ParserException();

            Read(DASH);

            var end = ReadWhile(char.IsDigit);
            return end == ""
                ? (ISubRange)new PrefixSubRange(long.Parse(start))
                : (ISubRange)new ClosedSubRange(long.Parse(start), long.Parse(end));
        }

        protected SuffixSubRange ParseSuffixRange()
        {
            Read(DASH);

            var end = ReadWhile(char.IsDigit);
            if (end == "") throw new ParserException();

            return new SuffixSubRange(long.Parse(end));
        }
    }
}
