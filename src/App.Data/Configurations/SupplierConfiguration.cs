using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier").HasKey(p => p.Id);

            builder
                .Property(p => p.Id)
                .HasColumnName("Id")
                .HasColumnType("varchar(50)")                
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(200)")                
                .IsRequired();

            builder
                .Property(p => p.Document)
                .HasColumnName("Document")
                .HasColumnType("varchar(50)");

            builder
                .Property(p => p.SupplierType)
                .HasColumnName("SupplierType")
                .HasColumnType("int")                
                .IsRequired();

            builder
                .Property(p => p.Active)
                .HasColumnName("Active")
                .HasColumnType("bit")                
                .IsRequired();

            builder
                .HasOne(p => p.Address)
                .WithOne(p => p.Supplier);

            builder
                .HasMany(p => p.Products)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);
        }
    }
}
