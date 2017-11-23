using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FMS.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace FMS.Core.Model
{
    public class JournalLineItem
    {
        [Key]
        public Guid Id { get; set; }
        public Journal Journal { get; set; }
        public JournalType Type { get; set; }
        public decimal Amount { get; set; }
        public string Economic { get; set; }
        public string Fund { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<JournalLineItem>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
