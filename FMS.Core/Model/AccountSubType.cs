using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FMS.Core.Model
{
    public class AccountSubType
    {
        [Key]
        public Guid Id { get; set; }
        public AccountGroup AccountGroup { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<AccountSubType>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }
    }
}
