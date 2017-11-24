using System;

namespace Accountant.DataAccess.Multitenancy {
    public class TenantInfoDto {
        public Guid TenantId { get; } = Guid.Empty;
        public TenantInfoDto(Guid tenantId)
        {
            TenantId = tenantId;
        }        
    }
}