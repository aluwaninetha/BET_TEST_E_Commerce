using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class ProductImage : AuditableBaseEntity
    {
        [Required]
        public Byte[] Image { get; set; }
    }
}
