using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Parsing
{
    public interface IHeaderReader<T>
    {
        T Read(string value);
    }
}
