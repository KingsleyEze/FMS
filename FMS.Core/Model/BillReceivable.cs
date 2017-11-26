using FMS.Utilities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class BillReceivable
    {
        [Key]
        public Guid Id { get; set; }
        public string PayeeId { get; set; }
        public string BillNumber { get; set; }
        public string Description { get; set; }
        public string Organisation { get; set; }
        public string Economic { get; set; }
        public string Fund { get; set; }
        public string GeoCode { get; set; }
        public string Function { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionDate { get; set; }
        public BillStatusType Status { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {

            builder.Entity<BillReceivable>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;
        }
    }
}
