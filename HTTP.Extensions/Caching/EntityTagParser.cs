using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Caching
{
    public class EntityTagParser : ParserBase, IHeaderParser<IEnumerable<EntityTag>>
    {
        private const string ANY_FLAG = "*";
        private const string WEAK_FLAG = "W/";
        private const string QUOTE = "\"";
        private const string COMMA = ",";

        public IEnumerable<EntityTag> Parse(string value)
        {
            Initialize(value);

            if (value.Trim() == ANY_FLAG)
            {
                return new[] { EntityTag.Any };
            }

            return ParseEntityTags().ToArray();
        }

        protected IEnumerable<EntityTag> ParseEntityTags()
        {
            if (!IsAtEnd())
            {
                yield return ParseEntityTag();
            }

            while (!IsAtEnd())
            {
                SkipWhiteSpaces();
                Read(COMMA);
                SkipWhiteSpaces();
                yield return ParseEntityTag();
            }
        }

        protected EntityTag ParseEntityTag()
        {
            bool isWeak = false;

            if (Peek(WEAK_FLAG))
            {
                isWeak = true;
                Read(WEAK_FLAG.Length);
            }
            Read(QUOTE);
            var tag = ReadUntil(QUOTE);
            Read(QUOTE);
            return new EntityTag(isWeak, tag);
        }
    }
}
