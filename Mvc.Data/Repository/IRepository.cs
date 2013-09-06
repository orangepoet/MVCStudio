using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Models.Entities;
using Mvc.Data.UnitOfWork;

namespace Mvc.Data.Repository {
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity {

        IUnitOfWork UnitOfWork { get; }
        IQueryable<TEntity> All { get; }
        TEntity Get(params object[] keys);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(params object[] keys);
        void Delete(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    
    }
}
