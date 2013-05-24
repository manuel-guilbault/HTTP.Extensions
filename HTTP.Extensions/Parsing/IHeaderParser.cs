using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Parsing
{
    public interface IHeaderParser<TResult>
    {
        TResult Parse(Tokenizer tokenizer);
    }
}
