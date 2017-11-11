using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FMS.Core.Model
{
    public class Country
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public HashSet<State> States => new HashSet<State>();
        public HashSet<PublicHoliday> PublicHolidays => new HashSet<PublicHoliday>();

        public override string ToString()
        {
            return Name;
        }


        public static void ConfigureFluent(ModelBuilder builder)
        {
            //builder.Entity<Country>().Property(b => b.Name).ForSqlServerHasColumnType("varchar(100)");
            builder.Entity<Country>().Property(b => b.IsActive).HasDefaultValue(true);
        }
    }
}
