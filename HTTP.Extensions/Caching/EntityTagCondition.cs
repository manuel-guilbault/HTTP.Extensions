using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Caching
{
    public class EntityTagCondition
    {
        public static readonly EntityTagCondition Any = new AnyEntityTag();

        private EntityTag[] validTags;

        internal EntityTagCondition()
        {
            validTags = new EntityTag[0];
        }

        public EntityTagCondition(params EntityTag[] validTags)
        {
            if (validTags == null) throw new ArgumentNullException("validTags");
            if (!validTags.Any()) throw new ArgumentException("validTags must not be empty", "validTags");
            if (validTags.Any(tag => tag == null)) throw new ArgumentException("validTags must not contain null values", "validTags");

            this.validTags = validTags.ToArray();
        }

        public IEnumerable<EntityTag> ValidTags
        {
            get { return validTags; }
        }

        public virtual bool IsValid(EntityTag entityTag, EntityTagComparisonType comparisonType = EntityTagComparisonType.Strong)
        {
            if (entityTag == null) throw new ArgumentNullException("entityTag");

            return IsValid(new[] { entityTag }, comparisonType);
        }

        public virtual bool IsValid(IEnumerable<EntityTag> entityTags, EntityTagComparisonType comparisonType = EntityTagComparisonType.Strong)
        {
            if (entityTags == null) throw new ArgumentNullException("entityTags");

            return validTags.Any(t1 => entityTags.Any(t2 => t1.Equals(t2, comparisonType)));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var entityTag in validTags)
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

    internal class AnyEntityTag : EntityTagCondition
    {
        public override bool IsValid(IEnumerable<EntityTag> entityTags, EntityTagComparisonType comparisonType = EntityTagComparisonType.Strong)
        {
            return entityTags.Any();
        }

        public override string ToString()
        {
            return "*";
        }
    }
}
