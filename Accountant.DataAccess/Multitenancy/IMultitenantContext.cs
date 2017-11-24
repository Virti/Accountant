using System;

namespace Accountant.DataAccess.Multitenancy {
    public interface IMultitenantContext {
        Guid TenantId { get; }
    }
}