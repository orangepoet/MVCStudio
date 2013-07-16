using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Models {
    public class UserInfo {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RegionID { get; set; }
        public string RegionName { get; set; }
    }
}
