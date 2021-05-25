using InvoiceGenerator.Entities;
using InvoiceGenerator.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;


namespace InvoiceGenerator.Repository.Configurations
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.HasMany<IItem>(u => u.Items)
                .WithOne(i => (Invoice)i.Invoice)
                .HasForeignKey(u => u.InvoiceNo)
                .IsRequired();

            builder.HasMany<IComment>(u => u.Comments)
                .WithOne(c => (Invoice)c.Invoice)
                .HasForeignKey(ur => ur.InvoiceNo);
        }
    }
}
