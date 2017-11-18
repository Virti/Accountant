using System;

namespace Accountant.Domain
{
    public interface IBaseEntity {
        Guid Id { get; set; }
    }
}
