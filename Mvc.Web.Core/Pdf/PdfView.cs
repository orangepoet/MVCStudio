using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;

namespace Mvc.Web.Core.Pdf {
    public class PdfView : IView, IViewEngine {
        private readonly ViewEngineResult _result;

        public PdfView(ViewEngineResult result) {
            _result = result;
        }

        public void Render(ViewContext viewContext, TextWriter writer) {
            // generate view into string
            var sb = new System.Text.StringBuilder();
            TextWriter tw = new System.IO.StringWriter(sb);
            _result.View.Render(viewContext, tw);
            var resultCache = sb.ToString();

            // detect itext (or html) format of response
            XmlParser parser;
            using (var reader = GetXmlReader(resultCache)) {
                while (reader.Read() && reader.NodeType != XmlNodeType.Element) {
                    // no-op
                }

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "itext")
                    parser = new XmlParser();
                else
                    parser = new HtmlParser();
            }

            // Create a document processing context
            var document = new Document();
            document.Open();

            // associate output with response stream
            var pdfWriter = PdfWriter.GetInstance(document, viewContext.HttpContext.Response.OutputStream);
            pdfWriter.CloseStream = false;

            // this is as close as we can get to being "success" before writing output
            // so set the content type now
            viewContext.HttpContext.Response.ContentType = "application/pdf";
            string fileName = viewContext.ViewData["FileName"] as string;
            viewContext.HttpContext.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName ?? "report" + ".pdf");

            // parse memory through document into output
            using (var reader = GetXmlReader(resultCache)) {
                parser.Go(document, reader);
            }

            pdfWriter.Close();
        }

        private static XmlTextReader GetXmlReader(string source) {
            byte[] byteArray = Encoding.UTF8.GetBytes(source);
            MemoryStream stream = new MemoryStream(byteArray);

            var xtr = new XmlTextReader(stream);
            xtr.WhitespaceHandling = WhitespaceHandling.None; // Helps iTextSharp parse 
            return xtr;
        }

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache) {
            throw new System.NotImplementedException();
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache) {
            throw new System.NotImplementedException();
        }

        public void ReleaseView(ControllerContext controllerContext, IView view) {
            _result.ViewEngine.ReleaseView(controllerContext, _result.View);
        }
    }
}
