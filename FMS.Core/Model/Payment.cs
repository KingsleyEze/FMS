using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FMS.Core.Model
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public BillPayable BillPayable { get; set; }
        public decimal Amount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public string Description { get; set; }
        public string TransactionDate { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<Payment>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
