using HTTP.Extensions.Caching;
using HTTP.Extensions.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class IfRangeParser : IHeaderParser<IfRange>
    {
        private IHeaderParser<EntityTag> entityTagParser;

        public IfRangeParser(IHeaderParser<EntityTag> entityTagParser)
        {
            if (entityTagParser == null) throw new ArgumentNullException("entityTagParser");

            this.entityTagParser = entityTagParser;
        }

        public IfRange Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

            var lastModified = tokenizer.StringValue.AsHttpDateTime();
            if (lastModified != null)
            {
                return new IfRange(lastModified.Value);
            }
            else
            {
                return new IfRange(entityTagParser.Parse(tokenizer));
            }
        }
    }
}
