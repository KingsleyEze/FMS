using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class State
    {
        [Key]
        public Guid Id { get; set; }
        public Country Country { get; set; }
        public string Name { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<State>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
