﻿using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Mvc.Web.Core.Pdf {
    public class PdfViewResult : ActionResult {
        private string _viewName;
        private object _model;
        private string _downloadName;

        public PdfViewResult(string viewName)
            : this(viewName, null) {
        }

        public PdfViewResult(string viewName, object model)
            : this(viewName, model, "report") {
        }

        public PdfViewResult(string viewName, object model, string downloadName = "report") {
            _viewName = viewName;
            _model = model;
            _downloadName = downloadName;
        }

        public override void ExecuteResult(ControllerContext context) {
            try {
                var response = context.HttpContext.Response;
                response.Clear();
                response.AddHeader("ContentType", "application/pdf");
                response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.pdf", _downloadName));
                using (Document document = new Document())
                using (PdfWriter pdfWriter = PdfWriter.GetInstance(document, response.OutputStream)) {
                    pdfWriter.CloseStream = false;


                    ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(context, _viewName, null);
                    if (viewEngineResult == null) {
                        throw new FileNotFoundException("View cannot be found!");
                    }
                    var view = viewEngineResult.View;
                    context.Controller.ViewData.Model = _model;

                    StringBuilder sb = new StringBuilder();
                    using (TextWriter tw = new StringWriter(sb)) {
                        var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, tw);
                        view.Render(ctx, tw);
                    }

                    string html = sb.ToString();
                    string pattern = "<a class=\"pdfexport\"\\s+href=\"[~/\\w]+\">\\w+</a>";
                    string s = Regex.Replace(html, pattern, "");
                    using (TextReader tr = new StringReader(s)) {
                        document.Open();
                        FontFactory.Register(context.HttpContext.Server.MapPath("~/Content/fonts/ARIALUNI.TTF"), "Arial Unicode MS");
                        XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, document, tr);
                        document.Close();
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
