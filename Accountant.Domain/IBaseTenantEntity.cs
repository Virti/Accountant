using System;

namespace Accountant.Domain
{
    public interface IBaseTenantEntity : IBaseEntity {
        Guid TenantId { get; set; }
    }
}