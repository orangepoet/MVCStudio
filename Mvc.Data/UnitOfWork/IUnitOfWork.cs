using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Data.UnitOfWork {
    public interface IUnitOfWork {
        int Commit();
        void Rollback();
    }
}
