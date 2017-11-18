namespace Accountant.Domain.Budget
{
    public class OperationCategory : BaseTenantEntity {

        public OperationKind Kind { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
    }
}