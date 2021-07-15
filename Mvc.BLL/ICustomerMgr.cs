using System;
namespace Mvc.BLL {
    public interface ICustomerMgr {
        // comments
        void AddCustomer(Mvc.Models.Entities.Customer c);
        Mvc.Models.Entities.Customer GetCustomer(int id);
        System.Linq.IQueryable<Mvc.Models.Entities.Customer> GetCustomers();
        void Update(Mvc.Models.Entities.Customer model);
    }
}
