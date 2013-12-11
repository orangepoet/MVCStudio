using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Mvc.Data;
using Mvc.Data.UnitOfWork;
using Mvc.Models.Entities;
using Ninject;

namespace Mvc.Data.UnitOfWork.Impl {

    public class EFRepositoryContext : IUnitOfWorkContext {

        private bool isCommitted = false;

        [Inject]
        public LogisticsEntities _context { get; set; }

        public DbSet<TEntity> Set<TEntity>() where TEntity : Entity {
            return _context.Set<TEntity>();
        }

        public void RegisterNew<TEntity>(TEntity entity) where TEntity : Entity {
            EntityState state = _context.Entry(entity).State;
            if (state == EntityState.Detached) {
                _context.Entry(entity).State = EntityState.Added;
            }
        }

        public void RegisterNew<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity {
            try {
                _context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities) {
                    RegisterNew(entity);
                }
            } finally {
                _context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void RegisterModified<TEntity>(TEntity entity) where TEntity : Entity {
            if (_context.Entry(entity).State == EntityState.Detached) {
                _context.Set<TEntity>().Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : Entity {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void RegisterDeleted<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity {
            try {
                _context.Configuration.AutoDetectChangesEnabled = false;
                foreach (TEntity entity in entities) {
                    RegisterDeleted(entity);
                }
            } finally {
                _context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public int Commit() {
            if (isCommitted) {
                return 0;
            }
            try {
                int result = _context.SaveChanges();
                isCommitted = true;
                return result;
            } catch (DbUpdateException) {
                //if (e.InnerException != null && e.InnerException.InnerException is SqlException) {
                //    SqlException sqlEx = e.InnerException.InnerException as SqlException;

                //}
                throw;
            }
        }

        public void Rollback() {
            _context.ChangeTracker.Entries()
                  .ToList()
                  .ForEach(entry => entry.State = System.Data.EntityState.Unchanged);
        }

        public void Dispose() {
            if (!isCommitted) {
                Commit();
            }
            _context.Dispose();
        }
    }
}
