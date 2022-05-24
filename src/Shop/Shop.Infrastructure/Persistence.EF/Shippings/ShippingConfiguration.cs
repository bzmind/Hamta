﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ShippingAggregate;

namespace Shop.Infrastructure.Persistence.EF.Shippings;

public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
{
    public void Configure(EntityTypeBuilder<Shipping> builder)
    {
        builder.ToTable("Shippings", "shipping");

        builder.Property(shipping => shipping.Id)
            .UseIdentityColumn(1);

        builder.Property(shipping => shipping.Method)
            .HasColumnName("Method")
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsOne(shipping => shipping.Cost, option =>
        {
            option.Property(cost => cost.Value)
                .HasColumnName("Cost")
                .IsRequired();
        });
    }
}