namespace FMS.Core.ViewModel.Payment
{
    public class SearchPaymentView
    {
    
        public string PayerId { get; set; }
        public string BillNumber { get; set; }
        public string Amount { get; set; }
        public string TransactionDate { get; set; }
        public dynamic SearchResult { get; set; }

    
    }
}
