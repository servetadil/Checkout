using Microsoft.EntityFrameworkCore;
using Checkout.PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Infrastructure.DatabaseFactory
{
    public class CheckoutWebDbContext : DbContext
    {
        public CheckoutWebDbContext(DbContextOptions<CheckoutWebDbContext> options)
            : base(options)
        {

        }

        public DbSet<Merchant> Merchants { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CheckoutWebDbContext).Assembly);
        }
    }
}
