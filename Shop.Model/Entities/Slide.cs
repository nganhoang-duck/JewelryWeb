using System;
using System.Collections.Generic;

namespace Shop.Model.Entities
{
    public partial class Slide
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Image { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int? DisplayOrder { get; set; }
        public bool Status { get; set; }
    }
}
