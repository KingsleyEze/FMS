using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMS.Core.Model
{
    public class PublicHoliday
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CountryId { get; set; }
        //public string IsActive { get; set; }//Ongoing, Completed, Rejected,Canceled,NotStarted
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
    }
}