using System;
using System.Web;

namespace Mvc.Web.Core.Utils {
    /// <summary>
    /// 二级域名Cookie处理（所有以.cnblog.cn结尾的，共享Cookie资源）
    /// </summary>
    public class CookieDomainModule : IHttpModule {
        public void Dispose() {
            //dispose
        }

        public void Init(HttpApplication context) {
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        private void context_EndRequest(object sender, EventArgs e) {
            HttpContext context = ((HttpApplication)sender).Context;
            HttpCookie cookie = context.Response.Cookies["cookie1"];
            if (cookie != null) {
                cookie.Domain = "cnblogs.com";
            }
        }
    }
}