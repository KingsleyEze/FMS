using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Models.Account
{
    public class UserDetailView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string LGA { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Fax { get; set; }
        public string PostalAddress { get; set; }
        public string Website { get; set; }
        public string FileId { get; set; }
    }
}
