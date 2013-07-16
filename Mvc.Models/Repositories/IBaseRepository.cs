using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Models.Repositories {
    public interface IBaseRepository<T> {
        IQueryable<U> GetList<U>() where U : class;
        IQueryable<T> All { get; }
        //IQueryable<T> GetList(string filterExpression, string sortExpression, string sortDirection, int pageIndex, int pageSize, int pagesCount);
        T Find(Func<T, bool> filter);
        T Find(params object[] keys);
        void Add(T model);
        void Update(T model);
        void Delete(T model);
        void Dispose();
        void SaveChanges();
    }
}
