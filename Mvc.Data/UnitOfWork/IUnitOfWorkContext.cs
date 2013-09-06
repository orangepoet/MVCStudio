using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Models.Entities;
using System.Data.Entity;

namespace Mvc.Data.UnitOfWork {
    public interface IUnitOfWorkContext : IUnitOfWork, IDisposable {

        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;
        void RegisterNew<TEntity>(TEntity entity) where TEntity : Entity;
        void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;
        void RegisterModified<TEntity>(TEntity entity) where TEntity : Entity;
        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : Entity;
        void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;
    }
}
