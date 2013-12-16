using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using Mvc.Data.Repository;
using Mvc.Data.Repository.Impl;
using Mvc.Data.UnitOfWork.Impl;
using Mvc.Web.Models;
using Ninject;

namespace Mvc.Web.Controllers {
    public class HomeController : AsyncController {
        //
        // GET: /Home/
        [Inject]
        public ICustomerRepository custRep { get; set; }

        [Inject]
        public IGroupRepository groupRep { get; set; }


        public ActionResult Index(string id) {
            return View("Index", (object)id);
            //string errMsg = res.ResourceManager.GetString("ErrMsg");
        }

        private void GetMenuFromXML(DoWorkEventArgs e) {
            e.Result = GetMenuFromXML();
        }

        public ActionResult Menu() {
            var model = GetMenuFromXML();
            return PartialView("_menu", model);
        }

        #region Async Actions

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

        #endregion Async Actions

        private IList<MenuItem> GetMenuFromXML() {
            //var list = service.GetList(null);
            //var model = new List<Menu>();
            //foreach (var menu in list.Where(m => m.BelongTo.Equals(1))) {
            //    List<Tab> tabs = new List<Tab>();
            //    foreach (var tab in list.Where(
            //                        m => m.BelongTo == menu.Id)) {
            //        tabs.Add(new Tab {
            //            Contoller = tab.Controller,
            //            Title = tab.Title
            //        });
            //    }
            //    model.Add(new Menu { Name = menu.Title, Tabs = tabs });
            //}
            //return model;
            XDocument xMenu = XDocument.Load(Server.MapPath("~/XML/menu.xml"));
            return xMenu.Root.Elements("menu")
                             .Select(m => new MenuItem {
                                 MenuName = m.Attribute("name").Value,
                                 Children = new List<MenuItem>(
                                               m.Elements("menu").Select(sm => new MenuItem {
                                                   MenuName = sm.Attribute("name").Value,
                                                   Controller = sm.Attribute("controller").Value,
                                                   Action = sm.Attribute("action").Value,
                                               })
                                             )
                             })
                             .ToList();
        }

        //Globalization / Change Language
        public ActionResult Change(string language) {
            const string defaultLanguage = "en-US";
            bool isdefault = language.ToLower() == defaultLanguage;
            string lang = isdefault ? "" : language;
            string root = Url.Content("~").TrimEnd('/');
            Session["ci"] = new CultureInfo(lang);
            string[] langs = new string[]{
                "en-US","zh-CN","zh-TW"
            };

            string referrerPath = langs.Aggregate(this.Request.UrlReferrer.PathAndQuery,
                                                  (current, c) => current.Replace(c + "", "")
                                      ).Replace(root, "");
            //string url = root+ string.IsNullOrWhiteSpace(lang + referrerPath) ? Url.Content("~/") : langStr + referrerPath;
            string url = "";
            if (isdefault) {
                url = string.Format("{0}/{1}", root, referrerPath.Trim('/'));
            } else {
                url = string.Format("{0}/{1}/{2}", root, lang, referrerPath.Trim('/'));
            }
            return this.Redirect(url);
        }

        public ActionResult AppointmentData(string id) {
            IEnumerable<Appointment> data = new[] {
                new Appointment { ClientName = "Joe", Date = DateTime.Parse("1/1/2012")},
                new Appointment { ClientName = "Joe", Date = DateTime.Parse("2/1/2012")},
                new Appointment { ClientName = "Joe", Date = DateTime.Parse("3/1/2012")},
                new Appointment { ClientName = "Jane", Date = DateTime.Parse("1/20/2012")},
                new Appointment { ClientName = "Jane", Date = DateTime.Parse("1/22/2012")},
                new Appointment {ClientName = "Bob", Date = DateTime.Parse("2/25/2012")},
            };
            if (!string.IsNullOrEmpty(id) && id != "All") {
                data = data.Where(e => e.ClientName == id);
            }
            return PartialView(data);
        }

        public JsonResult Book() {
            return Json(new { Msg = "Success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JavaScriptResult Book(FormCollection forms) {
            //return Json(new { Msg = "Success" }, "text/html",JsonRequestBehavior.DenyGet);
            return JavaScript("alert('ss')");
        }

        public class Appointment {
            public string ClientName { get; set; }
            public DateTime Date { get; set; }
        }

        // 测试UnitOfWork
        public void Test() {
            custRep.Add(new Mvc.Models.Entities.Customer {
                CustomerId = 1000, CustomerName = "test_data", Status = "A",
                ShortName = "test", Modifyuser = "sa", Modifytime = DateTime.Now
            });
            groupRep.Add(new Mvc.Models.Entities.Group {
                Groupid = 1000, Groupname = "test_data",
                Modifyuser = "sa", Modifytime = DateTime.Now, Status = "A"
            });
            int result = groupRep.UnitOfWork.Commit();

            Debug.WriteLine("custRep: " + (((EFRepositoryContext)custRep.UnitOfWork)._context.GetHashCode()));
            Debug.WriteLine("groupRep: " + (((EFRepositoryContext)groupRep.UnitOfWork)._context.GetHashCode()));
            Debug.WriteLine("result: " + result.ToString());
        }
    }
}