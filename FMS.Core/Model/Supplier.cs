using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }
        public AppUser AppUser { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public Country Country { get; set; }
        public State State { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string OfficePhone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Notes { get; set; }

    }
}
