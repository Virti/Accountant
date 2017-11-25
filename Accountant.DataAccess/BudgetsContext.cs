using Accountant.DataAccess.Multitenancy;
using Accountant.Domain;
using Accountant.Domain.Budget;
using Microsoft.EntityFrameworkCore;

namespace Accountant.DataAccess
{
    public class BudgetsContext : BaseMultitenantContext<BudgetsContext>
    {
        public DbSet<MonthBudget> MonthBudgets { get; set; }

        public DbSet<OperationCategory> OperationCategories { get; set; }
        public DbSet<Operation> Operations { get; set; }


        public BudgetsContext(DbContextOptions<BudgetsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}
