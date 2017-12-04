using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FMS.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace FMS.Core.Model
{
    public class LineItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountSubTypeId { get; set; }
        public AccountSubType AccountSubType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public AccountGroupType AccountGroupType { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<LineItem>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
