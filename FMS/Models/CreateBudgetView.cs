using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FMS.Core.Model;

namespace FMS.Models
{
    public class CreateBudgetView
    {
        public string TransactionDate { get; set; }
        [Required]
        public Guid Economic { get; set; }
        [Required]
        public Guid Fund { get; set; }
        [Required]
        [RegularExpression("^[0-9]+(,[0-9]+)*$", ErrorMessage = "Amount must be numeric")]
        public string Amount { get; set; }
        [MaxLength(140, ErrorMessage = "Description character can not exceed 140 characters")]
        [MinLength(10, ErrorMessage = "Description character should not be less than 10 characters")]
        [Required]
        public string Description { get; set; }

        public virtual IList<LineItem> LineItemList { get; set; }
        public virtual IList<BankAccount> BankAccountList { get; set; }
    }
}
