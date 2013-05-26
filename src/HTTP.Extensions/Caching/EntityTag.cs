using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HTTP.Extensions.Caching
{
    public class EntityTag
    {
        private bool isWeak;
        private string value;

        private string stringValue = null;

        public EntityTag(string value)
            : this(false, value)
        {
        }

        public EntityTag(bool isWeak, string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value == "") throw new ArgumentException("value cannot not be empty", "value");

            this.isWeak = isWeak;
            this.value = value;
        }

        public bool IsWeak
        {
            get { return isWeak; }
        }

        public string Value
        {
            get { return value; }
        }

		public override bool Equals(object other)
		{
			return Equals(other as EntityTag);
		}

        public bool Equals(EntityTag other, EntityTagComparison comparisonType = EntityTagComparison.Strong)
        {
            if (other == null) return false;

            switch (comparisonType)
            {
                case EntityTagComparison.Strong:
                    return !IsWeak && !other.IsWeak && Value == other.Value;

                case EntityTagComparison.Weak:
                    return Value == other.Value;

                default:
                    throw new InvalidProgramException("Unknown EntityTagComparisonType." + comparisonType);
            }
        }

        public override string ToString()
        {
            if (stringValue == null)
            {
                var builder = new StringBuilder();
                Append(builder);
                stringValue = builder.ToString();
            }
            return stringValue;
        }

        internal virtual void Append(StringBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException("builder");

            if (isWeak)
            {
                builder.Append("W/");
            }
            builder.Append("\"");
            builder.Append(value);
            builder.Append("\"");
        }
    }
}
