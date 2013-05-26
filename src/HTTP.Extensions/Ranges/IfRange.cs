using HTTP.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class IfRange
    {
        private IfRangeType type;
        private object value;

        public IfRange(DateTime lastModified)
        {
            type = IfRangeType.LastModified;
            value = lastModified;
        }

        public IfRange(EntityTag entityTag)
        {
            if (entityTag == null) throw new ArgumentNullException("entityTag");

            type = IfRangeType.EntityTag;
            value = entityTag;
        }

        public IfRangeType Type
        {
            get { return type; }
        }

        public EntityTag EntityTag
        {
            get
            {
                if (type != IfRangeType.EntityTag) throw new InvalidOperationException("Type must be EntityTag");
                return (EntityTag)value;
            }
        }

        public DateTime LastModified
        {
            get
            {
                if (type != IfRangeType.LastModified) throw new InvalidOperationException("Type must be LastModified");
                return (DateTime)value;
            }
        }

        public override string ToString()
        {
            switch (type)
            {
                case IfRangeType.EntityTag:
                    return EntityTag.ToString();

                case IfRangeType.LastModified:
                    return LastModified.AsHttpDateTime();

                default:
                    throw new InvalidProgramException("Unknown IfRangeType." + type);
            }
        }
    }
}
