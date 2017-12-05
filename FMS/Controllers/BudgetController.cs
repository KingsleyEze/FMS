using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Models.Budget;
using FMS.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using ExcelDataReader;
using System.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{

    [Authorize]
    public class BudgetController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public BudgetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var viewModel = _unitOfWork.BudgetsRepository.Items
                                    .Include(m => m.Economic).ToList();

            return View(viewModel);
        }



        public IActionResult CreateBudget()
        {
            var viewModel = new CreateBudgetView
            {
                LineItemList = _unitOfWork.LineItemsRepository.Items.ToList(),
            };

            viewModel.TransactionDate = DateTime.Now.ToString("dd/MM/yyyy");

            return View(viewModel);
        }

        public IActionResult SaveBudget(CreateBudgetView viewModel)
        {
            if (ModelState.IsValid)
            {
                var budget = new Budget
                {
                    TransactionDate = viewModel.TransactionDate,
                    Description = viewModel.Description,
                    Amount = decimal.Parse(viewModel.Amount),
                    EconomicId = viewModel.Economic,
                    Type = BudgetStatusType.DRAFT,
                };

                _unitOfWork.BudgetsRepository.Insert(budget);

                _unitOfWork.SaveChanges();


                TempData["AlertMessage"] = $"Your budget was created successfully.";

                return RedirectToAction("Index");
            }

            return View("CreateBudget", viewModel);
        }


        public IActionResult LoadBudget()
        {
            var viewModel = new LoadBudget();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveLoadBudget(LoadBudget viewModel)
        {

            var excel = viewModel.File;

            if (excel != null && excel.Length > 0)
            {

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
                    ModelState.AddModelError("File", "This file format is not supported");
                    return View();
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

                TempData["AlertMessage"] = $"Your budget was uploaded successfully.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult AmendBudget()
        {
            return RedirectToAction("Index");
        }

    }
}
