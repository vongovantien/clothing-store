using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Comment
    {
        public string? Id { get; set; }
        public int? ProductId { get; set; }
        public string? ParentId { get; set; }
        public string? Title { get; set; }
        public string? Rating { get; set; }
        public string? CreatedAt { get; set; }
        public string? ModifiedAt { get; set; }
        public string? Comment1 { get; set; }
        public int? UserCreated { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? UserCreatedNavigation { get; set; }
    }
}
