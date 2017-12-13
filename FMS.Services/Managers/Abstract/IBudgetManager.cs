using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.ViewModel.Budget;

namespace FMS.Services.Managers.Abstract
{
    public interface IBudgetManager
    {
        void UploadExcel(LoadBudget viewModel);
    }
}
