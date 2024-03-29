﻿using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Repository
{
    public class InGenDbContext : IdentityDbContext<User, Role, long>
    {
        public InGenDbContext(DbContextOptions<InGenDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the currencies.
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Gets or sets the invoices.
        /// </summary>

        public DbSet<Invoice> Invoices { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>()
               .HasOne<Invoice>(i => (Invoice)i.Invoice)
               .WithMany(i => i.Items)
               .HasForeignKey(i => i.InvoiceNo)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasOne<Currency>(i => (Currency)i.Currency)
                .WithMany()
                .HasForeignKey(i => i.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfiguration(new InvoiceConfiguration());
        }
    }
}
