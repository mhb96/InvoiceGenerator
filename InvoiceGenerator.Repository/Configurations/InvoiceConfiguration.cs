using InvoiceGenerator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InvoiceGenerator.Repository.Configurations
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.HasOne<User>(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId);

            builder.HasOne<Currency>(i => i.Currency)
                .WithMany()
                .HasForeignKey(i => i.CurrencyId);

            builder.HasMany<Item>(u => u.Items)
                .WithOne(i => (Invoice)i.Invoice)
                .HasForeignKey(u => u.InvoiceNo)
                .IsRequired();
        }
    }
}
