using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class ContentRangeParser : ParserBase, IHeaderParser<ContentRange>
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

        public ContentRange Parse(string value)
        {
            try
            {
                Initialize(value);

                SkipWhiteSpaces();
                var unit = ParseUnit();

                Read(SPACE);

                SkipWhiteSpaces();
                var range = ParseSubRange();

                Read(SLASH);

                var instanceLength = ParseInstanceLength();

                if (!IsAtEnd()) throw new ParserException();

                return new ContentRange(unit, range, instanceLength);
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
            var name = ReadUntil(SPACE);
            var unit = units.Get(name);
            if (unit == null) throw new ParserException();

            return unit;
        }

        protected ContentSubRange ParseSubRange()
        {
            if (Peek(ASTERISK))
            {
                Read(ASTERISK);
                return ContentSubRange.Unknown;
            }

            var start = ReadWhile(char.IsDigit);
            if (start == "") throw new ParserException();

            Read(DASH);

            var end = ReadWhile(char.IsDigit);
            if (end == "") throw new ParserException();

            return new ContentSubRange(long.Parse(start), long.Parse(end));
        }

        protected InstanceLength ParseInstanceLength()
        {
            if (Peek(ASTERISK))
            {
                Read(ASTERISK);
                return InstanceLength.Unknown;
            }

            var value = ReadWhile(char.IsDigit);
            if (value == "") throw new ParserException();

            return new InstanceLength(long.Parse(value));
        }
    }
}
