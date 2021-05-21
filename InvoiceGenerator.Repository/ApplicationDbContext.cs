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
    }
}
