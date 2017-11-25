using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Models.BillReceivable
{
    public class CreateReceivableView
    {
        public Guid Id { get; set; }
        [Required]
        public string PayeeId { get; set; }
        public string BillNumber { get; set; }
        [MaxLength(140)]
        [MinLength(1)]
        [Required]
        public string Description { get; set; }
        public string Organisation { get; set; }
        [Required]
        public string Economic { get; set; }
        [Required]
        public string Fund { get; set; }
        public string GeoCode { get; set; }
        public string Function { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        [Required]
        [RegularExpression("^[0-9]+(,[0-9]+)*$", ErrorMessage = "Amount must be numeric")]    
        public string Amount { get; set; }
        public string TransactionDate { get; set; }
    }
}
