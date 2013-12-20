using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Web.Core.Pdf;

namespace Mvc.Web.Controllers {
    public class PdfController : Controller {
        //
        // GET: /Pdf/

        public ActionResult Index() {
            return View();
        }

        public ActionResult Html() {
            return View();
        }

        public ActionResult PdfHtml() {
            return new PdfViewResult("Html", null, DateTime.Now.ToShortTimeString());
        }
    }
}
