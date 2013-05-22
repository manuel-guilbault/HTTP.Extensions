using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Caching
{
    public class EntityTagParserBase : ParserBase
    {
        private const string WEAK_FLAG = "W/";
        private const string QUOTE = "\"";

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
