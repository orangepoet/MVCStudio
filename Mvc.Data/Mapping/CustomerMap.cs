using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Mvc.Models.Entity;

namespace Mvc.Data.Models.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerId);

            // Properties
            this.Property(t => t.CustomerName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ShortName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Address)
                .HasMaxLength(50);

            this.Property(t => t.MnCode)
                .HasMaxLength(50);

            this.Property(t => t.Phones)
                .HasMaxLength(25);

            this.Property(t => t.Contract)
                .HasMaxLength(20);

            this.Property(t => t.Status)
                .HasMaxLength(2);

            this.Property(t => t.PostCode)
                .HasMaxLength(10);

            this.Property(t => t.Remarks)
                .HasMaxLength(400);

            this.Property(t => t.Manualno)
                .HasMaxLength(20);

            this.Property(t => t.Fexs)
                .HasMaxLength(20);

            this.Property(t => t.Banks)
                .HasMaxLength(500);

            this.Property(t => t.Account)
                .HasMaxLength(20);

            this.Property(t => t.Mobiles)
                .HasMaxLength(50);

            this.Property(t => t.Emails)
                .HasMaxLength(255);

            this.Property(t => t.Idcard)
                .HasMaxLength(20);

            this.Property(t => t.Modifyuser)
                .HasMaxLength(12);

            // Table & Column Mappings
            this.ToTable("Customers");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.CustomerName).HasColumnName("CustomerName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.MnCode).HasColumnName("MnCode");
            this.Property(t => t.Phones).HasColumnName("Phones");
            this.Property(t => t.Contract).HasColumnName("Contract");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.PostCode).HasColumnName("PostCode");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.Manualno).HasColumnName("Manualno");
            this.Property(t => t.Fexs).HasColumnName("Fexs");
            this.Property(t => t.Banks).HasColumnName("Banks");
            this.Property(t => t.Account).HasColumnName("Account");
            this.Property(t => t.Mobiles).HasColumnName("Mobiles");
            this.Property(t => t.Emails).HasColumnName("Emails");
            this.Property(t => t.Idcard).HasColumnName("Idcard");
            this.Property(t => t.Trade).HasColumnName("Trade");
            this.Property(t => t.Sign).HasColumnName("Sign");
            this.Property(t => t.Isbatch).HasColumnName("Isbatch");
            this.Property(t => t.LastExport).HasColumnName("LastExport");
            this.Property(t => t.Ispay).HasColumnName("Ispay");
            this.Property(t => t.Modifyuser).HasColumnName("Modifyuser");
            this.Property(t => t.Modifytime).HasColumnName("Modifytime");
        }
    }
}
