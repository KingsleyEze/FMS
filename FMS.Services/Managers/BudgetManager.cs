using System;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Core.ViewModel.Budget;
using FMS.Services.Managers.Abstract;

namespace FMS.Services.Managers
{
    public class BudgetManager : IBudgetManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public BudgetManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
