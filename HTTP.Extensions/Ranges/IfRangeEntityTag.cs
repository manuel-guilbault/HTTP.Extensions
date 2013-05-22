using HTTP.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.Extensions.Ranges
{
    public class IfRangeEntityTag : IfRange
    {
        private EntityTag entityTag;

        public IfRangeEntityTag(EntityTag entityTag)
        {
            if (entityTag == null) throw new ArgumentNullException("entityTag");

            this.entityTag = entityTag;
        }

        public EntityTag EntityTag
        {
            get { return entityTag; }
        }

        public override string ToString()
        {
            return entityTag.ToString();
        }
    }
}
