using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Mvc.Data.Models.Mapping;

namespace Mvc.Data.Models
{
    public partial class MvcAppContext : DbContext
    {
        static MvcAppContext()
        {
            Database.SetInitializer<MvcAppContext>(null);
        }

        public MvcAppContext()
            : base("Name=MvcAppContext")
        {
        }

        public DbSet<Charge> Charges { get; set; }
        public DbSet<ContractRate> ContractRates { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<VenueStaff> VenueStaffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ChargeMap());
            modelBuilder.Configurations.Add(new ContractRateMap());
            modelBuilder.Configurations.Add(new ContractMap());
            modelBuilder.Configurations.Add(new CustomerGroupMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new ProductBrandMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new RackMap());
            modelBuilder.Configurations.Add(new StoreMap());
            modelBuilder.Configurations.Add(new VenueStaffMap());
        }
    }
}
