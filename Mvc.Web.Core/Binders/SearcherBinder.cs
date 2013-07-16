using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Web.Core {
    public class Searcher : Dictionary<string, string> {
        public override string ToString() {
            StringBuilder sb = new StringBuilder("s");
            foreach (var key in Keys) {
                sb.Append(string.Format("&{0}={1}", key, this[key]));
            }

            return HttpContext.Current.Server.UrlEncode(sb.ToString().TrimEnd('&'));
        }
    }

    public class SearcherBinder : DefaultModelBinder {
        /// <summary>
        /// ModelBinder for Searcher object.
        /// </summary>
        /// <param name="controllerContext">ControllerContext</param>
        /// <param name="bindingContext">BindingContext</param>
        /// <returns>Searcher object.</returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var request = HttpContext.Current.Request;
            var searchReq = request.QueryString["searcher"];
            if (searchReq != null || request.Form.AllKeys.Length > 0) {
                Searcher searcher = new Searcher();
                if (searchReq != null) {
                    var searchString = HttpContext.Current.Server.UrlDecode(searchReq.ToString());
                    foreach (var filter in searchString.Split('&').Skip(1)) {
                        string[] section = filter.Split('=');
                        searcher.Add(section[0], section[1]);
                    }
                } else {
                    foreach (var key in request.Form.AllKeys.Where(k => k.IndexOf("ad.") == 0)) {
                        if (!string.IsNullOrWhiteSpace(request.Form[key])) {
                            searcher.Add(key.Replace("ad.", "").ToLower(), request.Form[key].Trim());
                        }
                    }
                }
                return searcher;
            } else {
                return base.BindModel(controllerContext, bindingContext);
            }
        }
    }
}