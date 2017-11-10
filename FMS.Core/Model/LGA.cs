using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class LGA
    {
        [Key]
        public Guid Id { get; set; }
        public State State { get; set; }
        public string Name { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<LGA>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
