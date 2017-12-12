using FMS.Utilities.Enums;

namespace FMS.Core.ViewModel.BillReceivable
{
    public class ReceivableDetailView
    {
        public Core.Model.BillReceivable Receivable { get; set; }
        public BillStatusType Type { get; set; }
        public string Comment { get; set; }
    }
}
