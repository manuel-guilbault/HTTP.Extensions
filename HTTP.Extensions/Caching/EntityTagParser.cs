using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Caching
{
    public class EntityTagParser : EntityTagParserBase, IHeaderParser<EntityTag>
    {
        public EntityTag Parse(string value)
        {
            try
            {
                Initialize(value);

                var result = ParseEntityTag();
                if (!IsAtEnd()) throw new ParserException();
                return result;
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
    }
}
