using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(p => p.Id);              

            builder
                .Property(p => p.Id)                
                .HasColumnName("Id")
                .HasColumnType("varchar(50)")                
                .IsRequired();

            builder
                .Property(p => p.SupplierId)
                .HasColumnName("SupplierId")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(200)")
                .IsRequired();

            builder
                .Property(p => p.Description)
                .HasColumnName("Description")
                .HasColumnType("varchar(1000)");

            builder
                .Property(p => p.Image)
                .HasColumnName("Image")
                .HasColumnType("varchar(100)");

            builder
                .Property(p => p.Value)
                .HasColumnName("Value")
                .HasColumnType("decimal")                
                .IsRequired();

            builder
                .Property(p => p.RegisterDate)
                .HasColumnName("RegisterDate")
                .HasColumnType("datetime")                
                .IsRequired();

            builder
                .Property(p => p.Active)
                .HasColumnName("Active")
                .HasColumnType("bit")                
                .IsRequired();
        }
    }
}
