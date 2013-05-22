using HTTP.Extensions.Ranges;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.MVC.Ranges.Readers
{
    public interface IRangeReader
    {
        RangeStream ReadRange(ISubRange range, Stream stream);
    }
}
