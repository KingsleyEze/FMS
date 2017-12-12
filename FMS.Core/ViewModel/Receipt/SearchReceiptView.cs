namespace FMS.Core.ViewModel.Receipt
{
    public class SearchReceiptView
    {
    
        public string PayeeId { get; set; }
        public string BillNumber { get; set; }
        public string Amount { get; set; }
        public string TransactionDate { get; set; }
        public dynamic SearchResult { get; set; }

    
    }
}
