using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Model;
using FMS.Core.ViewModel.BillPayable;

namespace FMS.Services.Managers.Abstract
{
    public interface IPayableManager
    {
        BillPayable Save(CreatePayableView viewModel);
        BillPayable SetWorkFlowStatus(PayableDetailView viewModel);
        BillPayable GetByGuidId(string id);
        List<BillPayable> GetByStatus(string status);
    }
}
