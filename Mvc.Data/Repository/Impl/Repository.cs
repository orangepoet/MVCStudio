using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Models.Entities;
using Mvc.Data.UnitOfWork;
using Mvc.Data.Repository;
using Ninject;

namespace Mvc.Data.Repository.Impl {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity {

        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }

        protected IUnitOfWorkContext Context {
            get {
                if (UnitOfWork is IUnitOfWorkContext) {
                    return UnitOfWork as IUnitOfWorkContext;
                }
                throw new Exception(string.Format("数据仓储上下文对象类型不正确，应为IRepositoryContext，实际为 {0}", UnitOfWork.GetType().Name));
            }
        }

        public IQueryable<TEntity> All {
            get { return Context.Set<TEntity>(); }
        }

        public TEntity Get(params object[] keys) {
            return Context.Set<TEntity>().Find(keys);
        }


        public void Add(TEntity entity) {
            Context.RegisterNew(entity);
        }

        public void Delete(TEntity entity) {
            Context.RegisterDeleted(entity);
        }

        public void Delete(params object[] keys) {
            TEntity entity = Context.Set<TEntity>().Find(keys);
            Context.RegisterDeleted(entity);
        }

        public void Delete(IEnumerable<TEntity> entities) {
            Context.RegisterDeleted(entities);
        }

        public void Update(TEntity entity) {
            Context.RegisterModified(entity);
        }

        public void Dispose() {
            if (Context != null) {
                Context.Dispose();
            }
        }

        
    }
}
