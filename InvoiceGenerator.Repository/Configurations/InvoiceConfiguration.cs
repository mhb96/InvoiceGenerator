using InvoiceGenerator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace InvoiceGenerator.Repository.Configurations
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.HasMany<Item>(u => u.Items)
                .WithOne(i => i.Invoice)
                .HasForeignKey(u => u.InvoiceNo)
                .IsRequired();

            builder.HasMany<Comment>(u => u.Comments)
                .WithOne(c => c.Invoice)
                .HasForeignKey(ur => ur.InvoiceNo);
        }
    }
}
