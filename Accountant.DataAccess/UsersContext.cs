using Accountant.Domain;
using Microsoft.EntityFrameworkCore;

namespace Accountant.DataAccess
{
    public class UsersContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
    }
}
