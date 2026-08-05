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
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Cost).HasColumnType("decimal(10, 2)");
            builder.Property(d => d.ShortName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(d => d.Description).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(d => d.DeliveryTime).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
