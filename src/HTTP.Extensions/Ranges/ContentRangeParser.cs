using HTTP.Extensions.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class ContentRangeParser : IHeaderParser<ContentRange>
    {
        private const string SPACE = " ";
        private const string SLASH = "/";
        private const string DASH = "-";
        private const string ASTERISK = "*";

        private RangeUnitRegistry units;

        public ContentRangeParser()
            : this(RangeUnitRegistry.Default)
        {
        }

        public ContentRangeParser(RangeUnitRegistry units)
        {
            if (units == null) throw new ArgumentNullException("units");

            this.units = units;
        }

        public ContentRange Parse(Tokenizer tokenizer)
        {
            tokenizer.SkipWhiteSpaces();
            var unit = ParseUnit(tokenizer);

            tokenizer.Read(SPACE);

            tokenizer.SkipWhiteSpaces();
            var range = ParseSubRange(tokenizer);

            tokenizer.Read(SLASH);

            var instanceLength = ParseInstanceLength(tokenizer);

            return new ContentRange(unit, range, instanceLength);
        }

        protected RangeUnit ParseUnit(Tokenizer tokenizer)
        {
            var name = tokenizer.ReadUntil(SPACE);

            var unit = units.Get(name);
            if (unit == null) tokenizer.Throw(string.Format("Unknown range unit '{0}'", name));

            return unit;
        }

        protected ContentSubRange ParseSubRange(Tokenizer tokenizer)
        {
            if (tokenizer.IsNext(ASTERISK))
            {
                tokenizer.Read(ASTERISK);
                return ContentSubRange.Unknown;
            }

            var start = tokenizer.ReadLong();
            tokenizer.Read(DASH);
            var end = tokenizer.ReadLong();

            return new ContentSubRange(start, end);
        }

        protected InstanceLength ParseInstanceLength(Tokenizer tokenizer)
        {
            if (tokenizer.IsNext(ASTERISK))
            {
                tokenizer.Read(ASTERISK);
                return InstanceLength.Unknown;
            }

            return new InstanceLength(tokenizer.ReadLong());
        }
    }
}
