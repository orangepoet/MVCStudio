using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Mvc.Models.Entity;

namespace Mvc.Data.Models.Mapping
{
    public class CustomerGroupMap : EntityTypeConfiguration<CustomerGroup>
    {
        public CustomerGroupMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Groupid, t.Customerid });

            // Properties
            this.Property(t => t.Groupid)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Customerid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CustomerGroup");
            this.Property(t => t.Groupid).HasColumnName("Groupid");
            this.Property(t => t.Customerid).HasColumnName("Customerid");
            this.Property(t => t.CreateUser).HasColumnName("CreateUser");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
        }
    }
}
