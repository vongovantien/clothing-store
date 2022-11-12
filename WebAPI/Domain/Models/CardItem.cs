using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class CardItem
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? CartId { get; set; }
        public decimal? Price { get; set; }
        public string? Discount { get; set; }
        public int? Quantity { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Note { get; set; }
    }
}
