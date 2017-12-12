using FMS.Utilities.Enums;

namespace FMS.Core.ViewModel.BillPayable
{
    public class PayableDetailView
    {
        public Core.Model.BillPayable Payable { get; set; }
        public BillStatusType Type { get; set; }
        public string Comment { get; set; }
    }
}
