using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace FMS.Core.Model
{
    public class City
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        //public bool? IsActive { get; set; }
        public State State { get; set; }

        public static void ConfigureFluent(ModelBuilder builder)
        {
            //builder.Entity<City>().Property(b => b.Name).ForSqlServerHasColumnType("varchar(100)");
            //builder.Entity<City>().Property(b => b.IsActive).HasDefaultValue(true);
        }
    }
}