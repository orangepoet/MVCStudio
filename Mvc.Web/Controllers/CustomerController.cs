using System;
using System.Linq;
using System.Web.Mvc;
using Mvc.BLL;
using Mvc.Models.Entities;
using Mvc.Web.Core.Utils;
using Mvc.Web.Core.Exts;
using Ninject;

namespace Mvc.Web.Controllers {
    //[Authorize]
    public class CustomerController : Controller {
        private const int pageSize = 10;

        [Inject]
        public ICustomerMgr bl { get; set; }

        //BL_Customer bl = new BL_Customer();

        //[AutoRefresh(DurationInSeconds = 10)]
        //[OutputCache(CacheProfile = "customercache")] 

        public PartialViewResult PopUp(int pageIndex = 1) {
            return PartialView(new PagedList<Customer>(
                                           bl.GetCustomers().Where(c => c.Status != "D"),
                                           pageIndex,
                                           10));
        }

        #region Basic CRUD

        public ViewResult Index() {
            //System.Diagnostics.Debug.WriteLine(pageIndex);
            //var model = service.All.Where(c => c.Status != "D").OrderByDescending(c => c.CustomerId).ToPagedList(pageIndex, pageSize);
            //return View(model);
            return View();
        }

        public PartialViewResult Grid(int pageIndex = 1) {
            //OutputCacheManager manager = new OutputCacheManager();

            //Debug.WriteLine(HttpRuntime.Cache.Count);
            //System.Threading.Thread.Sleep(2000);
            //Debug.WriteLine(HttpRuntime.Cache.Count);
            //PagedList<Customer> model = null;
            //string key = "customers/" + pageIndex.ToString();
            //if (HttpRuntime.Cache[key] != null) {
            //    System.Diagnostics.Debug.WriteLine("Cached:" + pageIndex.ToString());
            //    model = HttpRuntime.Cache[key] as PagedList<Customer>;
            //} else {
            //    model = rep.GetAll()
            //           .Where(c => c.Status != "D")
            //           .OrderByDescending(c => c.CustomerId)
            //           .ToPagedList(pageIndex, pageSize);
            //    SqlCacheDependency dependency = new SqlCacheDependency("Mvc", "Customers");
            //    System.Diagnostics.Debug.WriteLine("New:" + pageIndex.ToString());
            //    HttpRuntime.Cache.Insert(key, model, dependency);
            //}

            var model = bl.GetCustomers()
                       .Where(c => c.Status != "D")
                       .OrderByDescending(c => c.CustomerId)
                       .ToPagedList(pageIndex, pageSize);
            System.Diagnostics.Debug.WriteLine("Grid:" + pageIndex.ToString());
            return PartialView(model);
        }

        public ViewResult Details(int id) {
            return View(bl.GetCustomer(id));
        }

        public ActionResult Create() {
            return View(new Customer());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Customer c) {
            if (ModelState.IsValid) {
                try {
                    c.Modifytime = DateTime.Now;
                    c.Modifyuser = User.Identity.Name;
                    //rep.Add(c);
                    //rep.UnitOfWork.Commit();
                    bl.AddCustomer(c);
                    RedirectToAction("Index");
                } catch (Exception ex) {
                    ModelState.AddModelError("CreateError", ex.Message);
                }
            }
            return View(c);
        }

        public ActionResult Edit(int id) {
            return View(bl.GetCustomer(id));
        }

        [HttpPost]
        public ActionResult Edit(int CustomerId, FormCollection forms) {
            var model = bl.GetCustomer(CustomerId);
            if (TryUpdateModel(model)) {
                model.Modifyuser = User.Identity.Name;
                model.Modifytime = DateTime.Now;
                bl.Update(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id, int pageIndex) {
            try {
                var model = bl.GetCustomer(id);
                model.Status = "D";
                bl.Update(model);
            } catch (Exception) {
                throw;
            }

            return RedirectToAction("Grid", new { pageIndex = pageIndex });
        }

        public ActionResult Modify() {
            var m = bl.GetCustomers().FirstOrDefault(c => c.Status != "D");
            if (m != null) {
                m.Status = "D";
                bl.Update(m);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Export() {
            ExcelHelper.Write(new System.Data.DataTable(), ExcelTemplate.TEMPLATE1);
            return View();
        }
        #endregion
    }
}