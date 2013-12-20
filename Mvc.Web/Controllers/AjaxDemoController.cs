using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Web.Models;

namespace Mvc.Web.Controllers {
    public class AjaxDemoController : Controller {

        public ActionResult Index() {
            return View();
        }

        public ActionResult AppointmentData(string id) {
            IEnumerable<Appointment> data = new[] {
                new Appointment { ClientName = "Joe", Date = DateTime.Parse("1/1/2012")},
                new Appointment { ClientName = "Joe", Date = DateTime.Parse("2/1/2012")},
                new Appointment { ClientName = "Joe", Date = DateTime.Parse("3/1/2012")},
                new Appointment { ClientName = "Jane", Date = DateTime.Parse("1/20/2012")},
                new Appointment { ClientName = "Jane", Date = DateTime.Parse("1/22/2012")},
                new Appointment {ClientName = "Bob", Date = DateTime.Parse("2/25/2012")},
            };
            if (!string.IsNullOrEmpty(id) && id != "All") {
                data = data.Where(e => e.ClientName == id);
            }
            return PartialView(data);
        }

        public JsonResult Book() {
            return Json(new { Msg = "Success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JavaScriptResult Book(FormCollection forms) {
            //return Json(new { Msg = "Success" }, "text/html",JsonRequestBehavior.DenyGet);
            return JavaScript("alert('ss')");
        }
    }
}
