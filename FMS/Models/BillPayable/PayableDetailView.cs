using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Utilities.Enums;

namespace FMS.Models.BillPayable
{
    public class PayableDetailView
    {
        public Core.Model.BillPayable Payable { get; set; }
        public BillStatusType Type { get; set; }
        public string Comment { get; set; }
    }
}
