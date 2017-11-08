using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class Staff
    {
        [Key]
        public Guid Id { get; set; }
        public AppUser AppUser { get; set; }
        public string Title { get; set; }
        public string Rank { get; set; }
        public string GradeLevel { get; set; }
        public DateTime? DateOfFirstAppoint { get; set; }
        public DateTime? DateOfCurrentAppoint { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string Section { get; set; }
        public string Notes { get; set; }
    }
}
