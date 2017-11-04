using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Models.BillReceivable
{
    public class CreateReceivableView
    {
        public Guid Id { get; set; }
        public string PayeeId { get; set; }
        public string BillNumber { get; set; }
        public string Description { get; set; }
        public string Organisation { get; set; }
        public string Economic { get; set; }
        public string Fund { get; set; }
        public string GeoCode { get; set; }
        public string Function { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string TransactionDate { get; set; }
    }
}
