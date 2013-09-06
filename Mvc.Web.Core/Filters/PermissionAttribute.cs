using System;
using System.Linq;
using System.Web.Mvc;
using Mvc.Data.Repository;
using Mvc.Models;
using Mvc.Web.Core.Filters;
using Mvc.Web.Core.Utils;
using Mvc.Web.Core.Web;

namespace RSM.Portal.Infrastructure {
    /// <summary>
    /// Set Context before action result excution.
    /// </summary>
    public class PermissionAttribute : ActionFilterAttribute {
        IFunctionRoleRepository funcRepository = (IFunctionRoleRepository)DependencyResolver.Current.GetService(typeof(IFunctionRoleRepository));


        public enum AuthorizedFailedType {
            None,
            LoseUserinfo,
            NotAllowed,
        }

        private UserInfo currUser;
        public PermissionAttribute() {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            System.Diagnostics.Debug.Assert(filterContext != null);
            AuthorizedFailedType failedType;
            if (!this.AuthorizeCore(filterContext, out failedType)) {
                if (filterContext.IsChildAction) {
                    System.Diagnostics.Debug.WriteLine("child action not allowed.");
                    filterContext.Result = new EmptyResult();
                } else {
                    switch (failedType) {
                        case AuthorizedFailedType.LoseUserinfo:
                            filterContext.Result = new RedirectResult("~/System/LoseUserinfo");
                            break;
                        case AuthorizedFailedType.NotAllowed:
                            //if (filterContext.HttpContext.Request.IsAjaxRequest()) {
                            filterContext.Result = new RedirectResult("~/System/NotAllowed");
                            //} else {
                            //filterContext.Result = new RedirectResult("~/System/NotAllowed");
                            //}
                            break;
                    }
                }
            }

        }

        protected virtual bool AuthorizeCore(ActionExecutingContext filterContext, out AuthorizedFailedType failedType) {
            failedType = AuthorizedFailedType.None;

            var attrs = filterContext.ActionDescriptor.GetCustomAttributes(false);

            if (attrs.Any(attr => attr is AnonymousAttribute)) {
                return true;
            }

            currUser = SessionHelper.CurrUser;
            if (currUser == null) {
                System.Diagnostics.Debug.WriteLine("session lose");
                failedType = AuthorizedFailedType.LoseUserinfo;
                return false;
            } else {
                //session allowed
                if (attrs.Any(attr => attr is SessionAllowedAttribute)) {
                    return true;
                }

                var controller = filterContext.RouteData.Values["controller"].ToString();
                var action = filterContext.RouteData.Values["action"].ToString();
                //if (action == "Grid") {
                //    return true;
                //}
                var url = string.Format("~/{0}/{1}", controller, action);
                if (!filterContext.IsChildAction) {
                    SessionHelper.SetSession("url", url);
                }

                if (!funcRepository.IsAllowed(url, currUser.RoleId, currUser.UserID)) {
                    System.Diagnostics.Debug.WriteLine("not allowed with url: " + url);
                    LogHelper.WriteLog(string.Format("\r\n客户机IP:{0} \r\n访问不允许地址:{1}",
                        filterContext.HttpContext.Request.UserHostAddress, url));
                    failedType = AuthorizedFailedType.NotAllowed;
                    return false;
                } else {
                    return true;
                }
            }
        }
    }
}