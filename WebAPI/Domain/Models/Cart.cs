using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CardItems = new HashSet<CardItem>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? SessionId { get; set; }
        public string? Token { get; set; }
        public string? Status { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? Country { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Note { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<CardItem> CardItems { get; set; }
    }
}
