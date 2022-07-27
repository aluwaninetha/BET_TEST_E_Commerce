using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_Commerce.Domain.Entities
{
    public class OrderDetail : AuditableBaseEntity
    {
        public int OrderId { get; set; }
        public int PorductId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
