using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.OrderAggrecate;

namespace Talabat.Repository.Data.Configrations
{
    public class OrderItemsConfigrations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(orderitem => orderitem.Product, Product => Product.WithOwner());
            builder.Property(S => S.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
