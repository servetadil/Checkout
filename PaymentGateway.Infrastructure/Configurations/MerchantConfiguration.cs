using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Checkout.PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Infrastructure.Configurations
{
    public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.ToTable("Merchants", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.MerchantID)
                .HasColumnName("MerchantID").IsRequired().HasMaxLength(30);

            builder.Property(e => e.ApiKey)
                .HasColumnName("ApiKey").IsRequired();

            builder.Property(e => e.LastUpdatedDateTime)
                .HasColumnType("datetime");

            builder.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime").IsRequired();
        }
    }
}
