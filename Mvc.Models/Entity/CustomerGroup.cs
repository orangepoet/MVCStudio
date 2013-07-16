using System;
using System.Collections.Generic;

namespace Mvc.Models.Entity
{
    public partial class CustomerGroup
    {
        public string Groupid { get; set; }
        public int Customerid { get; set; }
        public string CreateUser { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
