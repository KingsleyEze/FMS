using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FMS.Core.Model;

namespace FMS.Models.BillPayable
{
    public class CreatePayableView
    {
        public Guid Id { get; set; }
        [Required]
        public string PayerId { get; set; }
        public string BillNumber { get; set; }
        [MaxLength(140)]
        [MinLength(10)]
        [Required]
        public string Description { get; set; }
        public string Organisation { get; set; }
        [Required]
        public Guid Economic { get; set; }
        [Required]
        public Guid Fund { get; set; }
        public string GeoCode { get; set; }
        public string Function { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        [Required]
        [RegularExpression("^[0-9]+(,[0-9]+)*$", ErrorMessage = "Amount must be numeric")]
        public string Amount { get; set; }
        public string TransactionDate { get; set; }

        public virtual IList<LineItem> LineItemList { get; set; }
        public virtual IList<BankAccount> BankAccountList { get; set; }
    }
}
