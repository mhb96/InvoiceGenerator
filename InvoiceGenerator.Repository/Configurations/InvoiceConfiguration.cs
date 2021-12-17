﻿using InvoiceGenerator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InvoiceGenerator.Repository.Configurations
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.HasOne<Image>(u => u.CompanyLogo)
                .WithMany()
                .HasForeignKey(u => u.CompanyLogoId);

            builder.HasMany<Item>(u => u.Items)
                .WithOne(i => (Invoice)i.Invoice)
                .HasForeignKey(u => u.InvoiceNo)
                .IsRequired();

            builder.HasMany<Comment>(u => u.Comments)
                .WithOne(c => (Invoice)c.Invoice)
                .HasForeignKey(ur => ur.InvoiceNo);
        }
    }
}
