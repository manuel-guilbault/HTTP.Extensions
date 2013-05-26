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

		protected EntityTag TryParseEntityTag(Tokenizer tokenizer)
		{
			try
			{
				return entityTagParser.Parse(tokenizer);
			}
			catch (ParsingException)
			{
				return null;
			}
		}

		protected DateTime? TryParseLastModified(Tokenizer tokenizer)
		{
			return tokenizer.TryReadDateTime();
		}

        public IfRange Parse(Tokenizer tokenizer)
        {
            if (tokenizer == null) throw new ArgumentNullException("tokenizer");

			var entityTag = TryParseEntityTag(tokenizer);
			if (entityTag != null)
			{
				return new IfRange(entityTag);
			}

			var lastModified = TryParseLastModified(tokenizer);
			if (lastModified != null)
			{
				return new IfRange(lastModified.Value);
			}

			tokenizer.Throw("Datetime or ETag expected");
			return null;
        }
    }
}
