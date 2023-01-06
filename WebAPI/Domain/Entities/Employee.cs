using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Employee
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? BirthDay { get; set; }
        public string? UserId { get; set; }
    }
}
