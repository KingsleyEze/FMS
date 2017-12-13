using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Core.ViewModel.Budget;
using FMS.Services.Managers.Abstract;
using FMS.Utilities.Enums;
using FMS.Utilities.StringKeys;
using Microsoft.EntityFrameworkCore;

namespace FMS.Services.Managers
{
    public class BudgetManager : IBudgetManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public BudgetManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Budget GetById(string budgetId)
        {
            Guid.TryParse(budgetId, out var id);

            return _unitOfWork.BudgetsRepository.Items.FirstOrDefault(x => x.Id == id);
        }

        public List<Budget> GetByLineItemId(Guid economicId)
        {
            return _unitOfWork.BudgetsRepository.Items
                        .Where(x =>  x.EconomicId == economicId).ToList();
        }

        public List<Budget> GetBudgets()
        {
            return _unitOfWork.BudgetsRepository.Items
                            .Include(m => m.Economic).ToList();
        }

        public void UploadExcel(LoadBudget viewModel)
        {
            var excel = viewModel.File;

            Stream stream = excel.OpenReadStream();

            IExcelDataReader reader = null;

            if (excel.FileName.EndsWith(".xls"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (excel.FileName.EndsWith(".xlsx"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else
            {
                //ModelState.AddModelError("File", "This file format is not supported");
                //return View();
            }

            DataSet result = reader.AsDataSet();

            DataTable dataTable = result.Tables[0];

            int numberOfRows = dataTable.Rows.Count;
            int numberOfColumn = dataTable.Columns.Count;

            for (int x = 1; x < numberOfRows; x++)
            {
                var lineItemCode = $"0{dataTable.Rows[x][0].ToString()}";

                var economic = _unitOfWork.LineItemsRepository.Items
                    .FirstOrDefault(l => l.Code == lineItemCode);

                if (economic != null)
                {
                    var budget = new Budget
                    {
                        TransactionDate = DateTime.Now.ToString("dd/MM/yyyy"),
                        EconomicId = economic.Id,
                        Description = dataTable.Rows[x][1].ToString(),
                        Amount = Convert.ToDecimal(dataTable.Rows[x][2].ToString()),
                    };

                    _unitOfWork.BudgetsRepository.Insert(budget);

                    _unitOfWork.SaveChanges();
                }
            }
        }

        public Budget Save(CreateBudgetView viewModel)
        {
            var budget = new Budget
            {
                TransactionDate = viewModel.TransactionDate,
                Description = viewModel.Description,
                Amount = decimal.Parse(viewModel.Amount),
                EconomicId = viewModel.Economic,
                Type = BudgetStatusType.DRAFT,
            };

            if (viewModel.Id != Guid.Empty)
            {
                budget.Id = viewModel.Id;
                _unitOfWork.BudgetsRepository.Update(budget);

                var history = new BudgetAmendHistory
                {
                    Budget = budget,
                    Amount = decimal.Parse(viewModel.PreviousAmount),
                    TransactionDate = DateTime.Now.ToString(DateFormatKey.Default)
                };

                _unitOfWork.BudgetAmendHistoriesRepository.Insert(history);
            }
            else
            {

                _unitOfWork.BudgetsRepository.Insert(budget);
            }


            _unitOfWork.SaveChanges();

            return budget;
        }
    }
}
