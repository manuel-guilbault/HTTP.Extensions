using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
	public class SubRange
	{
		public static SubRange CreateOffsetFromStart(long offset)
		{
			if (offset < 0) throw new ArgumentException("offset must be equal to or greater than zero", "offset");

			return new SubRange(SubRangeType.OffsetFromStart, offset, 0);
		}

		public static SubRange CreateClosedRange(long from, long to)
		{
			if (from < 0) throw new ArgumentException("from must be equal to or greater than zero", "from");
			if (to < from) throw new ArgumentException("to must be equal to or greater than from", "to");

			return new SubRange(SubRangeType.Closed, from, to);
		}

		public static SubRange CreateOffsetFromEnd(long offset)
		{
			if (offset <= 0) throw new ArgumentException("offset must be greater than zero", "offset");

			return new SubRange(SubRangeType.OffsetFromEnd, offset, 0);
		}

		private SubRange(SubRangeType type, long from, long to)
		{
			this.Type = type;
			this.From = from;
			this.To = to;
		}

		public SubRangeType Type { get; private set; }
		public long From { get; private set; }
		public long To { get; private set; }

		public override string ToString()
		{
			switch (Type)
			{
				case SubRangeType.OffsetFromStart:
					return From + "-";

				case SubRangeType.Closed:
					return string.Concat(From, "-", To);

				case SubRangeType.OffsetFromEnd:
					return "-" + From;

				default:
					throw new InvalidProgramException("Unknown SubRangeType." + Type);
			}
		}
	}
}
