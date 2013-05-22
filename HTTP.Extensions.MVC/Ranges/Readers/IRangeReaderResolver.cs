using HTTP.Extensions.Ranges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.MVC.Ranges.Readers
{
    public interface IRangeReaderResolver
    {
        IRangeReader Resolve(ISubRange range);
    }
}
