using System;
using System.Collections.Generic;

namespace Mvc.Models.Entity
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string MenuNo { get; set; }
        public Nullable<int> ParentId { get; set; }
    }
}
