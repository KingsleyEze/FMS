using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FMS.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace FMS.Core.Model
{
    public class Budget
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public virtual LineItem Economic { get; set; }
        public Guid EconomicId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionDate { get; set; }
        public BudgetStatusType Type { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<Budget>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
