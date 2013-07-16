using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Models.Repositories;
using Mvc.Models.Entity;

namespace Mvc.Data.RepositoryImpls {
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository {
    }
}
