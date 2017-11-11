using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FMS.Core.Model
{
    public class State
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<City> Cities { get; set; }
        //public bool? IsActive { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public static void ConfigureFluent(ModelBuilder builder)
        {
           // builder.Entity<State>().Property(b => b.IsActive).HasDefaultValue(true);
        }
    }
}
