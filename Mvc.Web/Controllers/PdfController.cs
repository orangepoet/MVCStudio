using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Web.Core.Pdf;

namespace Mvc.Web.Controllers {
    public class PdfController : Controller {
        public ViewResult Pdf() {
            return new PdfResult();
        }

        public ViewResult Pdf(object model) {
            return new PdfResult(model);
        }

        public ViewResult Pdf(object model, string viewName) {
            return new PdfResult(model, viewName);
        }

        public ViewResult Pdf(object model, string viewName, string pdfDownloadName) {
            return new PdfResult(model, viewName, pdfDownloadName);
        }
    }
}
