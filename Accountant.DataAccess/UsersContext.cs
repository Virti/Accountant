using Accountant.Domain;
using Accountant.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Accountant.DataAccess
{
    public class UsersContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<UserAccount>()
                .HasOne(u => u.Tenant)
                .WithMany()
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
