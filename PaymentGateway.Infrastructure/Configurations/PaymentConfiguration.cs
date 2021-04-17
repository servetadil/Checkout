using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Checkout.PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property<Guid>(e => e.PaymentID)
                .HasColumnName("PaymentID").IsRequired().HasMaxLength(36);

            builder.Property(e => e.OrderID)
                .HasColumnName("OrderID").IsRequired().HasMaxLength(36);

             builder.OwnsOne(x => x.Amount, b =>
            {
                b.Property(x => x.Value)
                    .HasColumnName("Amount")
                    .IsRequired().HasPrecision(18, 2); ;

                b.Property(x => x.Currency)
                    .HasColumnName("Currency")
                    .IsRequired()
                    .HasMaxLength(3);
            });

            builder.OwnsOne(x => x.Card, b =>
            {
                b.Property(x => x.CardName)
                    .HasColumnName("CardName");

                b.Property(x => x.CardNumber)
                    .HasColumnName("CardNumber");

                b.Property(x => x.ExpiryMonth)
                    .HasColumnName("ExpiryMonth");

                b.Property(x => x.ExpiryYear)
                    .HasColumnName("ExpiryYear");
            });

            builder.Property<DateTime>(e => e.PaymentDate)
                .HasColumnName("PaymentDate");

            builder.Property(e => e.IsFutureTransaction)
                    .HasColumnName("IsFutureTransaction")
                     .HasColumnType("bit");

            builder.Property(e => e.PaymentStatus)
                    .HasColumnName("PaymentStatus")
                    .HasColumnType("int")
                    .IsRequired();
        }
    }
}
