using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class ReceivableWorkFlow
    {
        [Key]
        public Guid Id { get; set; }
        public BillReceivable BillReceivable { get; set; }
        public string Comment { get; set; }
        public DateTime? Date { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {

            builder.Entity<ReceivableWorkFlow>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;
        }
    }
}
