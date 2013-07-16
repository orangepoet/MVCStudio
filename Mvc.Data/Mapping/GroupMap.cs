using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Mvc.Models.Entity;

namespace Mvc.Data.Models.Mapping
{
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Groupid);

            // Properties
            this.Property(t => t.Groupid)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Groupname)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Status)
                .HasMaxLength(20);

            this.Property(t => t.Modifyuser)
                .HasMaxLength(12);

            // Table & Column Mappings
            this.ToTable("Groups");
            this.Property(t => t.Groupid).HasColumnName("Groupid");
            this.Property(t => t.Groupname).HasColumnName("Groupname");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Modifyuser).HasColumnName("Modifyuser");
            this.Property(t => t.Modifytime).HasColumnName("Modifytime");
        }
    }
}
