using System.Collections.Generic;
using FMS.Core.Model;

namespace FMS.Core.ViewModel.Account
{
    public class SupplierDetailView
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string OfficePhone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Notes { get; set; }
        public IList<Country> CountryList { get; set; }
        public IList<State> StateList { get; set; }
    }
}
