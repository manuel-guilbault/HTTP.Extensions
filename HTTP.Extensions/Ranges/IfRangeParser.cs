using HTTP.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class IfRangeParser : EntityTagParserBase, IHeaderParser<IfRange>
    {
        public IfRange Parse(string value)
        {
            Initialize(value);

            var lastModified = value.AsHttpDateTime();
            if (lastModified != null)
            {
                return new IfRange(lastModified.Value);
            }
            else
            {
                return new IfRange(ParseEntityTag());
            }
        }
    }
}
