using System;
using System.Collections.Generic;

namespace Shop.Model.Entities
{
    public partial class MenuGroup
    {
        public MenuGroup()
        {
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}
