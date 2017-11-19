using System;
using Accountant.Domain.Budget;
using Microsoft.EntityFrameworkCore;

namespace Accountant.DataAccess
{
    public class BudgetsContext : DbContext {
        public DbSet<MonthBudget> MonthBudgets { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationCategory> OperationCategories { get; set; } 
    }
}
