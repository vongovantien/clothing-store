using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Product
    {
        public Product()
        {
            CardItems = new HashSet<CardItem>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public string? Discount { get; set; }
        public string? Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int? MenuId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Menu? Menu { get; set; }
        public virtual ICollection<CardItem> CardItems { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
