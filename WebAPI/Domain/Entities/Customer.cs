﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Customer
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Number { get; set; }
    }
}
