using System;
using System.Web.Mvc;
using NLog.Mvc;

namespace Mvc.Web.Core.Filters {
    public class LogFilter : ActionFilterAttribute {
        private ILogger logger = new NLog.Mvc.Logger();
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            //log request url
            logger.Trace(filterContext.HttpContext.Request.Url.ToString());
            base.OnActionExecuting(filterContext);
        }
    }
}