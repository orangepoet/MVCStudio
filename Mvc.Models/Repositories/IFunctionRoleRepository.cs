using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Models.Repositories {
    public interface IFunctionRoleRepository {
        bool IsAllowed(string url, string roleID, string userID);
    }
}
