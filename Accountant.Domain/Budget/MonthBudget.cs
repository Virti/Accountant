using System;
using System.Collections.Generic;

namespace Accountant.Domain.Budget
{
    public class MonthBudget : BaseTenantEntity {
        public int Year { get; set; }
        public int Month { get; set; }

        public ICollection<Operation> Operations { get; set; }
    }
}