﻿using System;
using System.Collections.Generic;

namespace Assignment2.Models
{
    public partial class Author
    {
        public int AuthorId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? EmailAddress { get; set; }
    }
}
