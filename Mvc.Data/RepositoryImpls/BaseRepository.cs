using System;
using System.Linq;
using Mvc.Models.Repositories;

namespace Mvc.Data.RepositoryImpls {
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable
            where T : class {
        protected MvcEntities db = new MvcEntities();

        public IQueryable<T> All {
            get {
                return db.Set<T>();
            }
        }

        public T Find(params object[] keys) {
            return db.Set<T>().Find(keys);
        }

        public T Find(Func<T, bool> filter) {
            return db.Set<T>().FirstOrDefault(filter);
        }

        public void Add(T model) {
            db.Set<T>().Add(model);
        }

        public void Delete(T model) {
            db.Entry(model).State = System.Data.EntityState.Deleted;
        }

        public void Update(T model) {
            db.Entry(model).State = System.Data.EntityState.Modified;
        }

        public void SaveChanges() {
            try {
                db.SaveChanges();
            } catch (System.Data.Entity.Validation.DbEntityValidationException ex) {
                var errMsgs = ex.EntityValidationErrors
                               .SelectMany(e => e.ValidationErrors)
                               .Select(ve => string.Format("Property: {0}, ErrorMessage: {1}\r\n", ve.PropertyName, ve.ErrorMessage))
                               .Aggregate((result, each) => result += each);
                throw new Exception(errMsgs, ex);
            }
        }

        public void Dispose() {
            db.Dispose();
        }

        public IQueryable<U> GetList<U>() where U : class {
            return db.Set<U>();
        }
    }
}