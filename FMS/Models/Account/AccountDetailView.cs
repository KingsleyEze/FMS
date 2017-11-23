using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Models.Account
{
    public class AccountDetailView
    {
        public UserDetailView UserDetail { get; set; }
        public BankDetailView BankDetail { get; set; }
        public StaffDetailView StaffDetail { get; set; }
        public SupplierDetailView SupplierDetail { get; set; }

        private class AppUserView
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        
    }
}
