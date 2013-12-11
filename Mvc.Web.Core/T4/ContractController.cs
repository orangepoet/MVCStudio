using System;
using System.Linq;
using System.Web.Mvc;

namespace Lib.Mvc4App.Controllers {
    using Models;
    using Services;
    using Infrastructure;

    public class ContractController : BaseController {
        private readonly IContractService service;

        public ContractController(IContractService service) {
            this.service = service;
        }

        public ViewResult Index(int pageIndex = 1) {
            return View(new PagedList<Contract>(
                                            service.GetList(c => c.Status != "D"),
                                            pageIndex,
                                            pageSize));
        }

        public ViewResult Details(int id) {
            return View(service.Find(id));
        }

        public ActionResult Create() {
            return View(new Contract());
        }

        [HttpPost]
        public ActionResult Create(Contract model) {
            if (ModelState.IsValid) {
				model.Status = "A";
				model.Modifyuser = User.Identity.Name;
                model.Modifytime = DateTime.Now;
                service.Add(model);
				service.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id) {
            return View(service.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(int id,FormCollection forms) {
			var model = service.Find(id);
            if (TryUpdateModel(model)) {
                model.Modifyuser = User.Identity.Name;
                model.Modifytime = DateTime.Now;
                service.Update(model);
                service.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public PartialViewResult Delete(int id, int pageIndex) {
            var model = service.Find(id);
            model.Status = "D";
            service.Update(model);
			service.Save();
            var list = new PagedList<Contract>(
                    service.GetList(c => c.Status != "D")
                    , pageIndex
                    , pageSize
                );

            return PartialView("_list", list);
        }

        protected override void Dispose(bool disposing) {
            if (disposing)
            {
                service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
