using FMS.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FMS.Core.Model
{
    public class AppData
    {
        [Key]
        public Guid Id { get; set; }
        public AppDataType Key { get; set; }
        public string Value { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<AppData>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
