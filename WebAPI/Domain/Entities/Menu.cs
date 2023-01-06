using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public string? Slug { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
