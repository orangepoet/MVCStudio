using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Web.Areas.Admin.Controllers {
    public class SysUserController : Controller {
        //
        // GET: /Admin/SysUser/

        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            return View();
        }

    }
}
