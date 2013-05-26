using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Parsing
{
    public static class TokenizerExtensions
    {
        public static long ReadLong(this Tokenizer tokenizer)
        {
            var numberPosition = tokenizer.Position;
            var value = tokenizer.ReadWhile(char.IsDigit);
            if (value == "") tokenizer.Throw("Digit expected");

            try
            {
                return long.Parse(value);
            }
            catch (OverflowException)
            {
                tokenizer.Throw("Number overflow", -numberPosition);
                return 0;
            }
        }

        public static long? TryReadLong(this Tokenizer tokenizer)
        {
            var numberPosition = tokenizer.Position;
            var value = tokenizer.ReadWhile(char.IsDigit);
            if (value == "") return null;

            try
            {
                return long.Parse(value);
            }
            catch (OverflowException)
            {
                tokenizer.Throw("Number overflow", -numberPosition);
                return 0;
            }
        }

		public static DateTime? TryReadDateTime(this Tokenizer tokenizer)
		{
			var value = tokenizer.ToString();

			DateTime dateTime;
			if (DateTime.TryParseExact(value, "r", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime))
			{
				tokenizer.Read(value);
				return dateTime;
			}
			else
			{
				return null;
			}
		}
    }
}
