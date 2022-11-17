using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Gmail { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Role { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Intro { get; set; }
        public string? Profile { get; set; }
        public string? Avatar { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
