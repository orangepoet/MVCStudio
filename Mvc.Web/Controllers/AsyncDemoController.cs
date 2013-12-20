using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Web.Controllers {
    // Here use AsyncController to apply Async;
    public class AsyncDemoController : AsyncController {
        //
        // GET: /AsyncDemo/

        public ActionResult Index() {
            return View();
        }

        public void DataAsync() {
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(() => {
                AsyncManager.Parameters["Data"] = "DataAsync";
                AsyncManager.OutstandingOperations.Decrement();
            });
        }

        public ActionResult DataCompleted(string Data) {
            return Content(Data);
        }
    }
}
