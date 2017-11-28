using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class PayableWorkFlow
    {
        [Key]
        public Guid Id { get; set; }
        public BillPayable BillPayable { get; set; }
        public string Comment { get; set; }
        public DateTime? Date { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {

            builder.Entity<PayableWorkFlow>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;
        }
    }
}
