using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Caching
{
    public class EntityTagConditionParser : EntityTagParserBase, IHeaderParser<EntityTagCondition>
    {
        private const string ANY_FLAG = "*";
        private const string COMMA = ",";

        public EntityTagCondition Parse(string value)
        {
            try
            {
                Initialize(value);

                if (value.Trim() == ANY_FLAG)
                {
                    return EntityTagCondition.Any;
                }
                else
                {
                    return new EntityTagCondition(ParseEntityTags().ToArray());
                }
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

        protected IEnumerable<EntityTag> ParseEntityTags()
        {
            SkipWhiteSpaces();
            yield return ParseEntityTag();
        
            while (!IsAtEnd())
            {
                SkipWhiteSpaces();
                Read(COMMA);
                SkipWhiteSpaces();
                yield return ParseEntityTag();
            }
        }
    }
}
