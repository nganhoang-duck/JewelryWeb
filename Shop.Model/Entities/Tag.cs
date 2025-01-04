using System;
using System.Collections.Generic;

namespace Shop.Model.Entities
{
    public partial class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}
