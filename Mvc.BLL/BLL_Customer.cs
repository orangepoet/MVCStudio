using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Models.Entities;
using Mvc.Data.Repository;
using Ninject;
using System.Linq.Expressions;

namespace Mvc.BLL {
    public class BLL_Customer : Mvc.BLL.IBLL_Customer {

        [Inject]
        public ICustomerRepository rep { get; set; }

        public void AddCustomer(Customer c) {
            rep.Add(c);
            rep.UnitOfWork.Commit();
        }

        public IQueryable<Customer> GetCustomers() {
            return rep.All;
        }

        public Customer GetCustomer(int id) {
            return rep.Get(id);
        }

        public void Update(Customer model) {
            rep.Update(model);
            rep.UnitOfWork.Commit();
        }
    }
}
