using System.Web.Mvc;

namespace Mvc.Web.Areas.Visitor {
    public class VisitorAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Visitor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "Visitor_default",
                "Visitor/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
