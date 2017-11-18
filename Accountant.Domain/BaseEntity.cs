using System;
using System.ComponentModel.DataAnnotations;

namespace Accountant.Domain
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
