using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FMS.Core.Model
{
    public class BudgetAmendHistory
    {
        [Key]
        public Guid Id { get; set; }
        public Budget Budget { get; set; }
        public decimal Amount { get; set; }
        public string TransactionDate { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<Budget>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
