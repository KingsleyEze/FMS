using System.Collections.Generic;

namespace FMS.Core.ViewModel.Payment
{
    public class AddPaymentView
    {
        public string TransactionDate { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public Core.Model.BillPayable Payable { get; set; }
        public string BillNumber { get; set; }

        public IList<Core.Model.Payment> Payments { get; set; }
    }
}
