using System;
using System.ComponentModel.DataAnnotations;

namespace Accountant.Domain
{
    public class BaseTenantEntity : BaseEntity, IBaseTenantEntity
    {
        [Required]
        public Guid TenantId { get; set; }
    }
}