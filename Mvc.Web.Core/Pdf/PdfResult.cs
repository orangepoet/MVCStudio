using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mvc.Web.Core.Pdf {
    public class PdfResult : ViewResult {
        //Constructors
        public PdfResult(object model = null, string name = "", string pdfDownloadName = "report") {
            if (model == null)
                model = new ViewDataDictionary();
            ViewData = new ViewDataDictionary(model);
            ViewData.Add("FileName", pdfDownloadName);
            ViewName = name;
        }

        //Override FindView to load PdfView
        protected override ViewEngineResult FindView(ControllerContext context) {
            var result = base.FindView(context);
            if (result.View == null)
                return result;

            var pdfView = new PdfView(result);
            return new ViewEngineResult(pdfView, pdfView);
        }
    }
}
