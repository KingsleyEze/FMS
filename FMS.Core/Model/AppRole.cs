using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class AppRole
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<AppRole>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
