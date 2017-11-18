using Accountant.Domain;
using Microsoft.EntityFrameworkCore;

namespace Accountant.DataAccess
{
    public class TenantContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
    }
}
