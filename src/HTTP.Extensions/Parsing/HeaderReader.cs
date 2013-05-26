using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Parsing
{
    public class HeaderReader<T> : IHeaderReader<T>
    {
        private IHeaderParser<T> parser;

        public HeaderReader(IHeaderParser<T> parser)
        {
            if (parser == null) throw new ArgumentNullException("parser");

            this.parser = parser;
        }

        public T Read(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            var tokenizer = new Tokenizer(value);
            var result = parser.Parse(tokenizer);
            if (!tokenizer.IsAtEnd())
            {
                tokenizer.Throw("End of string expected");
            }
            return result;
        }
    }
}
