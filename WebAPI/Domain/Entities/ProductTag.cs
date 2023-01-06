using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class ProductTag
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? TagId { get; set; }

        public virtual Tag? Tag { get; set; }
    }
}
