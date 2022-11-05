using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public string? Slug { get; set; }
    }
}
