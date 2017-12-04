using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Models;
using FMS.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }



        public IActionResult CreateBudget()
        {
            var viewModel = new CreateBudgetView
            {
                LineItemList = _unitOfWork.LineItemsRepository.Items.ToList(),

                BankAccountList = _unitOfWork.BankAccountsRepository.Items.ToList(),
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
                    FundId = viewModel.Fund,
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
            return View();
        }

        public IActionResult AmendBudget()
        {
            return View();
        }

    }
}
