using Accountant.DataAccess.Multitenancy;
using Accountant.Domain.Budget;
using Microsoft.EntityFrameworkCore;

namespace Accountant.DataAccess
{
    public class BudgetsContext : BaseMultitenantContext
    {
        public DbSet<MonthBudget> MonthBudgets { get; set; }

        public DbSet<OperationCategory> OperationCategories { get; set; }
        public DbSet<Operation> Operations { get; set; }
        

        public BudgetsContext(TenantInfoDto tenantInfoDto, DbContextOptions<UsersContext> options) : base(tenantInfoDto, options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}
