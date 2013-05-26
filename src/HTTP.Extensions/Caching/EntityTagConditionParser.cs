using HTTP.Extensions.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Caching
{
    public class EntityTagConditionParser : IHeaderParser<EntityTagCondition>
    {
        private const string ANY_FLAG = "*";
        private const string COMMA = ",";

        private IHeaderParser<EntityTag> entityTagParser;

        public EntityTagConditionParser()
            : this(new EntityTagParser())
        {
        }

        public EntityTagConditionParser(IHeaderParser<EntityTag> entityTagParser)
        {
            if (entityTagParser == null) throw new ArgumentNullException("entityTagParser");

            this.entityTagParser = entityTagParser;
        }

        public EntityTagCondition Parse(Tokenizer tokenizer)
        {
            if (tokenizer.IsNext(ANY_FLAG))
            {
                return EntityTagCondition.Any;
            }
            else
            {
                return new EntityTagCondition(ParseEntityTags(tokenizer).ToArray());
            }
        }

        protected IEnumerable<EntityTag> ParseEntityTags(Tokenizer tokenizer)
        {
            tokenizer.SkipWhiteSpaces();
            yield return entityTagParser.Parse(tokenizer);

            while (!tokenizer.IsAtEnd())
            {
                tokenizer.SkipWhiteSpaces();
                tokenizer.Read(COMMA);
                tokenizer.SkipWhiteSpaces();
                yield return entityTagParser.Parse(tokenizer);
            }
        }
    }
}
