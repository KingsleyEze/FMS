using System.Collections.Generic;
using FMS.Core.Model;

namespace FMS.Core.ViewModel.Account
{
    public class BankDetailView
    {
        public string Bank { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BVN { get; set; }
        public string TIN { get; set; }
        public IList<Bank> BankList { get; set; }
    }
}
