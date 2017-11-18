using System;

namespace Accountant.Domain.Budget
{
    public class Operation : BaseTenantEntity {
        public string Summary { get; set; }
        public string Remarks { get; set; }
        public decimal Value { get; set; }

        public OperationKind Kind { get; set; }

        public OperationCategory Category { get; set; }
        public Guid CategoryId { get; set; }

        public DateTime UtcDateTime { get; set; }
    }
}