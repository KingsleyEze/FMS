using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FMS.Core.Model;

namespace FMS.Core.ViewModel.Budget
{
    public class CreateBudgetView
    {
        public Guid Id { get; set; }
        public string TransactionDate { get; set; }
        [Required]
        public Guid Economic { get; set; }
        [Required]
        [RegularExpression("^\\$?\\d{1,3}(?:,\\d{3})*(?:\\.\\d{1,2})?$", ErrorMessage = "Amount must be numeric with comma")]
        public string Amount { get; set; }
        [MaxLength(140, ErrorMessage = "Description character can not exceed 140 characters")]
        [MinLength(10, ErrorMessage = "Description character should not be less than 10 characters")]
        [Required]
        public string Description { get; set; }

        public string PreviousAmount { get; set; }

        public virtual IList<LineItem> LineItemList { get; set; }
    }
}
