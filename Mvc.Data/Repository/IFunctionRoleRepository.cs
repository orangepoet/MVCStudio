using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Data.Repository {
    public interface IFunctionRoleRepository {
        bool IsAllowed(string url, string userid, string roleid);
    }
}
