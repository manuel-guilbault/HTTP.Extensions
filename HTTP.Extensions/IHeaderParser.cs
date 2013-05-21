using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions
{
    public interface IHeaderParser<TResult>
    {
        TResult Parse(string value);
    }
}
