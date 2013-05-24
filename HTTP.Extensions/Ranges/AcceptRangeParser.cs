using HTTP.Extensions.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class AcceptRangeParser : IHeaderParser<AcceptRange>
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

        public AcceptRange Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            if (tokenizer.IsNext(NONE))
            {
                return AcceptRange.None;
            }

            return new AcceptRange(ParseUnits(tokenizer).ToArray());
        }

        protected IEnumerable<RangeUnit> ParseUnits(Tokenizer tokenizer)
        {
            yield return ParseUnit(tokenizer);
            tokenizer.SkipWhiteSpaces();

            while (!tokenizer.IsAtEnd())
            {
                yield return ParseUnit(tokenizer);
                tokenizer.SkipWhiteSpaces();
            }
        }

        protected RangeUnit ParseUnit(Tokenizer tokenizer)
        {
            var name = tokenizer.ReadUntil(COMMA);

            var unit = units.Get(name);
            if (unit == null) tokenizer.Throw(string.Format("Unknown range unit '{0}'", name));

            return unit;
        }
    }
}
