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
    }
}
