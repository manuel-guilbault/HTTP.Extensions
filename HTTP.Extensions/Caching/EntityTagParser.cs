using HTTP.Extensions.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Caching
{
    public class EntityTagParser : IHeaderParser<EntityTag>
    {
        private const string WEAK_FLAG = "W/";
        private const string QUOTE = "\"";

        public EntityTag Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            bool isWeak = false;
            if (tokenizer.IsNext(WEAK_FLAG))
            {
                isWeak = true;
                tokenizer.Read(WEAK_FLAG);
            }

            tokenizer.Read(QUOTE);
            var tag = tokenizer.ReadUntil(QUOTE);
            tokenizer.Read(QUOTE);

            return new EntityTag(isWeak, tag);
        }
    }
}
