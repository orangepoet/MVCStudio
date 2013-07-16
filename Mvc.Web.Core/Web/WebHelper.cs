using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace Mvc.Web.Core.Web {
    public class WebHelper {
        /// <summary>
        /// 判断网络资源是否存在
        /// </summary>
        /// <param name="url">资源地址</param>
        /// <returns></returns>
        public static bool CheckWebResourceExist(string url) {
            bool exist = false;
            HttpWebResponse response = null;
            WebRequest request = HttpWebRequest.Create(url);
            request.Method = "HEAD";
            request.Credentials = CredentialCache.DefaultCredentials;
            try {
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK) {
                    exist = true;
                }
            } catch (WebException) {
                //资源不存在
            } finally {
                //关闭响应
                if (response != null) {
                    response.Close();
                }
            }

            return exist;
        }
    }
}