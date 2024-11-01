using System;
using System.Collections.Generic;

namespace Shop.Model.Entities
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? CategoryId { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public bool Status { get; set; }
        public int? ViewCount { get; set; }
        public DateTime WriteDate { get; set; }

        public virtual PostCategory? Category { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
	}
}
