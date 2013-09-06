using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Mvc.Models.Entities;

namespace Mvc.Data.Models.Mapping
{
    public class MenuMap : EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DisplayName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.MenuNo)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Menus");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.MenuNo).HasColumnName("MenuNo");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
        }
    }
}
