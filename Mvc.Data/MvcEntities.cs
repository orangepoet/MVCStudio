using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Models.Entities;
using System.Data.Entity;

namespace Mvc.Data {
    public class MvcEntities : DbContext {
        public MvcEntities()
            : this("MvcEntities") {

        }

        public MvcEntities(string connName)
            : base(connName) {
            Database.SetInitializer<MvcEntities>(null);
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}
