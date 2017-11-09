using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FMS.Core.Model
{
    public class AppUserProfile
    {
        [Key]
        public Guid Id { get; set; }
        public AppUser AppUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public LGA LGA { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Fax { get; set; }
        public string PostalAddress { get; set; }
        public string Website { get; set; }
        public string FileId { get; set; }
    }
}
