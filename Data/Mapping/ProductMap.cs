using EComerceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EComerceApi.Data.Mapping;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id);
            
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        builder
            .Property(x => x.Price)
            .IsRequired()
            .HasColumnName("Price")
            .HasColumnType("int");
        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreateAt")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();
        builder
            .Property(x => x.InStock)
            .HasColumnName("InStock")
            .HasDefaultValue(true);
        builder
            .HasIndex(x => x.Id, "IX_Product_Id");

    }
}