using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address").HasKey(p => p.Id);

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
                .Property(p => p.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar(200)")       
                .IsRequired();

            builder
                .Property(p => p.Number)
                .HasColumnName("Number")
                .HasColumnType("varchar(50)")       
                .IsRequired();

            builder
                .Property(p => p.Complement)
                .HasColumnName("Complement")
                .HasColumnType("varchar(50)");

            builder
                .Property(p => p.ZipCode)
                .HasColumnName("ZipCode")
                .HasColumnType("varchar(50)");

            builder
                .Property(p => p.District)
                .HasColumnName("District")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(p => p.City)
                .HasColumnName("City")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(p => p.State)
                .HasColumnName("State")
                .HasColumnType("varchar(50)")
                .IsRequired();
        }
    }
}
