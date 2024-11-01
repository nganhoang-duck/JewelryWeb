using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model.Entities
{
	public partial class Team
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Sayings { get; set; }
		public string Image { get; set; } = null!;
		public string Career { get; set; } = null!;
	}
}
