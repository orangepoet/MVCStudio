using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mvc.Web.Core.Pdf;

namespace Mvc.Web.Core.Exts {
    public static class ExtController {
        public static ViewResult Pdf(this Controller controller) {
            return new PdfResult();
        }

        public static ViewResult Pdf(this Controller controller
            , object model) {
            return new PdfResult(model);
        }

        public static ViewResult Pdf(this Controller controller
            , object model, string viewName) {
            return new PdfResult(model, viewName);
        }

        public static ViewResult Pdf(this Controller controller
            , object model, string viewName, string pdfDownloadName) {
            return new PdfResult(model, viewName, pdfDownloadName);
        }
    }
}
