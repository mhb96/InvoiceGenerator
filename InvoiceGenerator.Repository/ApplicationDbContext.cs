using InvoiceGenerator.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.Repository
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, long>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the comments
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets thr invoices.
        /// </summary>

        public DbSet<Invoice> Invoices { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public DbSet<Item> Items { get; set; }

        // No need to have configuration file for small stuff
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Invoice>().HasQueryFilter(e => !e.IsDeleted);

            builder.Entity<Item>()
               .HasOne<Invoice>(i => (Invoice)i.Invoice).WithMany(i => i.Items)
               .HasForeignKey(i => i.InvoiceNo)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>()
             .HasOne<Invoice>(i => (Invoice)i.Invoice).WithMany(c => c.Comments)
             .HasForeignKey(i => i.InvoiceNo)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
