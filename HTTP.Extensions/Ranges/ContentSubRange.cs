using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class ContentSubRange
    {
        public static readonly ContentSubRange Unknown = new UnknownContentSubRange();

        private long startAt;
        private long endAt;

        internal ContentSubRange()
        {
        }

        public ContentSubRange(long startAt, long endAt)
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

    internal class UnknownContentSubRange : ContentSubRange
    {
        public override string ToString()
        {
            return "*";
        }
    }
}
