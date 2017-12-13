using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Model;
using FMS.Core.ViewModel.BillPayable;
using FMS.Core.ViewModel.BillReceivable;

namespace FMS.Services.Managers.Abstract
{
    public interface IReceivableManager
    {
        BillReceivable Save(CreateReceivableView viewModel);
        BillReceivable SetWorkFlowStatus(ReceivableDetailView viewModel);
        BillReceivable GetByGuidId(string id);
        List<BillReceivable> GetByStatus(string status);
    }
}
