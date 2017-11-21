using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Accountant.Domain.Users;

namespace Accountant.Domain
{
    public class BaseTenantEntity : BaseEntity, IBaseTenantEntity
    {
        [Required]
        public Guid TenantId { get; set; }
        
        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }
    }
}