using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Mvc.Web.Core.Utils;

namespace Mvc.Web.Models {
    public class GridView<T> {
        public PagedList<T> PagedList { get; set; }
        public Searcher Searcher { get; set; }
    }

    public class Searcher : Dictionary<string, string> {
        public override string ToString() {
            StringBuilder sb = new StringBuilder("s");
            foreach (var key in Keys) {
                sb.Append(string.Format("&{0}={1}", key, this[key]));
            }

            return HttpContext.Current.Server.UrlEncode(sb.ToString().TrimEnd('&'));
        }
    }
}