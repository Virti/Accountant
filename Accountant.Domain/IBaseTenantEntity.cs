using System;

namespace Accountant.Domain
{
    public interface IBaseTenantEntity : IBaseEntity {
        Tenant Tenant { get; set; }
        Guid TenantId { get; set; }
    }
}