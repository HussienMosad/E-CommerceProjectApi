using Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace persistence.Data.Configrations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Price).HasColumnType("decimal(18,4)");
            builder.OwnsOne(o => o.ProductInOrderItem, p => p.WithOwner());
        }
    }
}
