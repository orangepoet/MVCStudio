using System;
using System.Collections.Specialized;
using System.Web;

namespace Mvc.Web.Core.Web {
    /// <summary>
    /// Cookie辅助类
    /// </summary>
    public class CookieHelper {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookieName">cookiename</param>
        public static void ClearCookie(string cookieName) {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null) {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        /// 
        public static HttpCookie GetCookie(string cookieName){
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                return cookie;
            }
            else
            {
                return null;
            }
        }
   /// <summary>
   /// Get the cookie value based on the specified name.
   /// </summary>
   /// <param name="cookiename">cookiename</param>
   /// <returns>cookie value string or an empty string if cookie not found.</returns>
        public static string GetCookieValue(string cookiename) {
            var cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null) {
                str = cookie.Value;
            }

            return str;
        }

        /// <summary>
        /// Get a specified value from a cookie collection.
        /// </summary>
        /// <param name="cookieName">cookie collection name</param>
        /// <param name="name">an item name of the cookie collection</param>
        /// <returns>The cookie value string or an empty string if cookie item does not exist in the cookie collection.</returns>
        public static string GetCookieValue(string cookieName, string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Values[name];
            }
            return str;
        }

        public static NameValueCollection GetCookieValues(string cookiename) {
            HttpCookie cookie = new HttpCookie(cookiename);
            if (cookie.HasKeys) {
                return cookie.Values;
            }
            return null;
        }

        /// <summary>
        /// 添加一个Cookie（24小时过期）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        public static void SetCookie(string cookiename, string cookievalue) {
            SetCookie(cookiename, cookievalue, DateTime.Now.AddDays(1.0));
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires) {
            HttpCookie cookie = new HttpCookie(cookiename) {
                Value = cookievalue,
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        ///添加一个cookie数组
        /// <param name="cookiename">cookie名</param>
        /// <param name="strValues">cookie值</param>
        /// <param name="expires">过期时间 days</param>
        public static void SetCookie(string cookiename, string[] strCookies, string[] strValues, int days) {
            HttpCookie cookie = new HttpCookie(cookiename) { Expires = DateTime.Now.AddDays(days) };
            for (int i = 0; i < strCookies.Length; i++) {
                cookie[strCookies[i]] = strValues[i];
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}