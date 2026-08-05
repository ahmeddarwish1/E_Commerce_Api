using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce_Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Subtotal).HasColumnType("decimal(10,2)");
            builder.Property(e => e.BuyerEmail).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(50);
            builder.OwnsOne(e => e.ShippingAdress);
        }
    }
}
