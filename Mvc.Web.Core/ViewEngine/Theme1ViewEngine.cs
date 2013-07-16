using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Web.Core.ViewEngine {
    public class Theme1ViewEngine : RazorViewEngine {
        public Theme1ViewEngine(string key = "") {
            string path = string.IsNullOrWhiteSpace(key) || key == "default" ? "~/Views" : "~/Themes/" + key;
            ViewLocationFormats = new string[] { 
                path + "/{1}/{0}.cshtml",
                path + "/{1}/{0}.vbhtml",
                path + "/Shared/{0}.cshtml",
                path + "/Shared/{0}.vbhtml",
            };

            PartialViewLocationFormats = ViewLocationFormats;
        }
    }
}