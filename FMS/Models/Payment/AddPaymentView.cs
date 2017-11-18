using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Models.Payment
{
    public class AddPaymentView
    {
        public string TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Core.Model.BillPayable Payable { get; set; }
        public string BillNumber { get; set; }

        public IList<Core.Model.Payment> Payments { get; set; }
    }
}
