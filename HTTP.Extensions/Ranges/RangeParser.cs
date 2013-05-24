using HTTP.Extensions.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class RangeParser : IHeaderParser<Range>
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

        public Range Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            tokenizer.SkipWhiteSpaces();
            var unit = ParseUnit(tokenizer);

            tokenizer.SkipWhiteSpaces();
            tokenizer.Read(EQUAL);

            var ranges = ParseRanges(tokenizer).ToArray();

            return new Range(unit, ranges);
        }

        protected RangeUnit ParseUnit(Tokenizer tokenizer)
        {
            var unitName = tokenizer.ReadUntil(EQUAL);

            var unit = units.Get(unitName);
            if (unit == null) tokenizer.Throw(string.Format("Unknown range unit '{0}'", unitName));

            return unit;
        }

        protected IEnumerable<ISubRange> ParseRanges(Tokenizer tokenizer)
        {
            yield return ParseRange(tokenizer);

            while (!tokenizer.IsAtEnd())
            {
                tokenizer.SkipWhiteSpaces();
                tokenizer.Read(COMMA);

                tokenizer.SkipWhiteSpaces();
                yield return ParseRange(tokenizer);
            }
        }

        protected ISubRange ParseRange(Tokenizer tokenizer)
        {
            if (tokenizer.IsNext(DASH))
            {
                return ParseSuffixRange(tokenizer);
            }

            var start = tokenizer.ReadLong();
            tokenizer.Read(DASH);
            var end = tokenizer.TryReadLong();

            return end == null
                ? (ISubRange)new PrefixSubRange(start)
                : (ISubRange)new ClosedSubRange(start, end.Value);
        }

        protected SuffixSubRange ParseSuffixRange(Tokenizer tokenizer)
        {
            tokenizer.Read(DASH);
            var end = tokenizer.ReadLong();

            return new SuffixSubRange(end);
        }
    }
}
