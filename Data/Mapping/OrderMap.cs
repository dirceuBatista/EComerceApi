using EComerceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EComerceApi.Data.Mapping;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id);

        builder
            .Property(x => x.OrderDate)
            .IsRequired()
            .HasColumnName("OrderDate")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValueSql("GETDATE()");
        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId)
            .HasConstraintName("FK_Customer_Order")
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(x=>x.OrderItems)
            .WithOne(x=>x.Order)
            .HasForeignKey(x => x.OrderId)
            .HasConstraintName("FK_Orderitem_Order_Id")
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}