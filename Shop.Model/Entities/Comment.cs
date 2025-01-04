using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Entities
{
	public partial class Comment
	{
		public int? Id { get; set; }
		public int PostId { get; set; }
		public string? Reader { get; set; }
		public string? ReaderImage { get; set; }
		public string? Email { get; set; }
		public string? CommentContent { get; set; }
		public DateTime CommentDate { get; set; }
		public virtual Post? Post { get; set; }
	}
}
