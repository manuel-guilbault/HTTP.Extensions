using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HTTP.Extensions.Caching
{
    public class EntityTag
    {
        public static readonly EntityTag Any = new AnyEntityTag();

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

    internal class AnyEntityTag : EntityTag
    {
        public AnyEntityTag()
            : base("*")
        {
        }

        internal override void Append(StringBuilder builder)
        {
 	         builder.Append("*");
        }
    }

    public static class EntityTagExtensions
    {
        public static string ToString(this IEnumerable<EntityTag> entityTags)
        {
            var builder = new StringBuilder();
            foreach (var entityTag in entityTags)
            {
                if (builder.Length > 0)
                {
                    builder.Append(", ");
                }
                entityTag.Append(builder);
            }
            return builder.ToString();
        }
    }
}
