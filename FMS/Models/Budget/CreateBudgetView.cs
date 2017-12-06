using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FMS.Core.Model;

namespace FMS.Models.Budget
{
    public class CreateBudgetView
    {
        public Guid Id { get; set; }
        public string TransactionDate { get; set; }
        [Required]
        public Guid Economic { get; set; }
        [Required]
        [RegularExpression("^[0-9]+(,[0-9]+)*$", ErrorMessage = "Amount must be numeric")]
        public string Amount { get; set; }
        [MaxLength(140, ErrorMessage = "Description character can not exceed 140 characters")]
        [MinLength(10, ErrorMessage = "Description character should not be less than 10 characters")]
        [Required]
        public string Description { get; set; }

        public string PreviousAmount { get; set; }

        public virtual IList<LineItem> LineItemList { get; set; }
    }
}
