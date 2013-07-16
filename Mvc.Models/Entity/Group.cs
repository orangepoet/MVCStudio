using System;
using System.Collections.Generic;

namespace Mvc.Models.Entity
{
    public partial class Group
    {
        public string Groupid { get; set; }
        public string Groupname { get; set; }
        public string Status { get; set; }
        public string Modifyuser { get; set; }
        public Nullable<System.DateTime> Modifytime { get; set; }
    }
}
