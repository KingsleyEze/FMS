using System.Collections.Generic;
using FMS.Core.Model;
using FMS.Utilities.Enums;

namespace FMS.Core.ViewModel.Account
{
    public class UserDetailView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public UserType UserType { get; set; }
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
        public IList<Country> CountryList { get; set; }
        public IList<State> StateList { get; set; }
    }
}
