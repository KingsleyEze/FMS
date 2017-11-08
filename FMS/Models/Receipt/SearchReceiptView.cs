using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Core.Model;

namespace FMS.Models.Receipt
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
