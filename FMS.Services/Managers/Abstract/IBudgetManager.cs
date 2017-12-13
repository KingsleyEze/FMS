using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Model;
using FMS.Core.ViewModel.Budget;

namespace FMS.Services.Managers.Abstract
{
    public interface IBudgetManager
    {
        void UploadExcel(LoadBudget viewModel);
        List<Budget> GetBudgets();
        Budget Save(CreateBudgetView viewModel);
        Budget GetById(string budgetId);
        List<Budget> GetByLineItemId(Guid economicId);
    }
}
