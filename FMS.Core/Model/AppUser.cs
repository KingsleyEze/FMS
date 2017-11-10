using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FMS.Core.Model
{
    public class AppUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            builder.Entity<AppUser>().Property(b => b.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWSEQUENTIALID()").Metadata.IsReadOnlyAfterSave = true;

        }

    }
}
