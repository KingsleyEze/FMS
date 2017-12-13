using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.ViewModel.Budget;
using FMS.Services.Managers.Abstract;
using FMS.Utilities.StringKeys;


namespace FMS.Controllers
{

    [Authorize]
    public class BudgetController : Controller
    {
        
        private readonly IBudgetManager _budgetManager;
        private readonly ILineItemManager _itemManager;
        
        public BudgetController(IBudgetManager budgetManager, ILineItemManager itemManager)
        {
            _budgetManager = budgetManager;
            _itemManager = itemManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = _budgetManager.GetBudgets();

            return View(viewModel);
        }
        
        [HttpGet]
        public IActionResult CreateBudget()
        {
            var viewModel = new CreateBudgetView
            {
                LineItemList = _itemManager.GetListItems(),
                TransactionDate = DateTime.Now.ToString(DateFormatKey.Default),
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveBudget(CreateBudgetView viewModel)
        {
            if (ModelState.IsValid)
            {
                var lineItem = _budgetManager.GetByLineItemId(viewModel.Economic);

                if (lineItem != null)
                {
                    viewModel.LineItemList = _itemManager.GetListItems();

                    ModelState.AddModelError("Economic", CreateBudgetView.EconomicExistError);

                    return View("CreateBudget", viewModel);
                }

                _budgetManager.Save(viewModel);

                TempData["AlertMessage"] = "Your budget was saved successfully.";

                return RedirectToAction("Index");
            }

            viewModel.LineItemList = _itemManager.GetListItems();

            return View("CreateBudget", viewModel);
        }
        
        [HttpGet]
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
                _budgetManager.UploadExcel(viewModel);

                TempData["AlertMessage"] = $"Your budget was uploaded successfully.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AmendBudget(string budgetId)
        {

            var budget = _budgetManager.GetById(budgetId);

            if(budget == null) return RedirectToAction("Index");

            var viewModel = new CreateBudgetView
            {
                Id = budget.Id,
                TransactionDate = budget.TransactionDate,
                Description = budget.Description,
                Economic = budget.EconomicId,
                Amount = budget.Amount.ToString(AmountFormatKey.Default),
                PreviousAmount = budget.Amount.ToString(AmountFormatKey.Default),
                LineItemList = _itemManager.GetListItems(),
            };
            
            return View("CreateBudget", viewModel);
        }

    }
}
