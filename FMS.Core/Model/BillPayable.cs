﻿using FMS.Utilities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class BillPayable
    {
        [Key]
        public  Guid Id { get; set; }
        public string PayerId { get; set; }
        public string BillNumber { get; set; }
        public string Description { get; set; }
        public string Organisation { get; set; }
        public virtual LineItem Economic { get; set; }
        public Guid EconomicId { get; set; }
        public virtual BankAccount Fund { get; set; }
        public Guid FundId { get; set; }
        public string GeoCode { get; set; }
        public string Function { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionDate { get; set; }
        public BillStatusType Status { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<BillPayable>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
