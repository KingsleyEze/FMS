using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FMS.Core.Model
{
    public class Journal
    {
        [Key]
        public Guid Id { get; set; }
        public string TransactionDate { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public string Economic { get; set; }
        public string Fund { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<Journal>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
