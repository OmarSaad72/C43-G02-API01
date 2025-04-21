using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderEntities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.OrderEntities.Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, a => a.WithOwner());
            builder.HasMany(o => o.OrderItems).WithOne();
            builder.Property(o => o.PaymentStatus).HasConversion(ps => ps.ToString(), // Store In DB
                p => Enum.Parse<OrderPaymentStatus>(p));  // Retrieve From DB
            builder.HasOne(o => o.DeliveryMethods).WithMany().OnDelete(DeleteBehavior.SetNull);
            builder.Property(o => o.Subtotal).HasColumnType("decimal(18, 3)");
        }
    }
}
