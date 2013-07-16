using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Models.Repositories {
    public interface IViewRepository {
        IQueryable<U> GetList<U>() where U : class;
    }
}
