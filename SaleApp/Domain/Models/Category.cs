using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Note { get; set; }
    }
}
