using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Utilities.Enums;

namespace FMS.Models.BillReceivable
{
    public class ReceivableDetailView
    {
        public Core.Model.BillReceivable Receivable { get; set; }
        public BillStatusType Type { get; set; }
        public string Comment { get; set; }
    }
}
