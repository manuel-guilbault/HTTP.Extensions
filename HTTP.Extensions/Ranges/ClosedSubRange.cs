using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class ClosedSubRange : ISubRange
    {
        private readonly long startAt;
        private readonly long endAt;

        public ClosedSubRange(long startAt, long endAt)
        {
            if (startAt < 0) throw new ArgumentException("startAt must be equal to or greater than zero", "startAt");
            if (endAt < startAt) throw new ArgumentException("endAt must be equal to or greater than startAt", "endAt");

            this.startAt = startAt;
            this.endAt = endAt;
        }

        public long StartAt
        {
            get { return startAt; }
        }

        public long EndAt
        {
            get { return endAt; }
        }

        public override string ToString()
        {
            return startAt + "-" + endAt;
        }
    }
}
