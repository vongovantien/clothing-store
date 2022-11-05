using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Tag
    {
        public Tag()
        {
            ProductTags = new HashSet<ProductTag>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Tag1 { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
