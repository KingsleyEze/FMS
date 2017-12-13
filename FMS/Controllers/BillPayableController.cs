using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Core;
using AutoMapper;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Core.ViewModel.BillPayable;
using FMS.Extensions;
using FMS.Services.Managers.Abstract;
using FMS.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using FMS.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{ 

    [Authorize]
    public class BillPayableController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPayableManager _payableManager;
        private readonly ILineItemManager _itemManager;
        private readonly IBankAccountManager _bankAccountManager;

        public BillPayableController(IUnitOfWork unitOfWork, IPayableManager payableManager, 
                                    ILineItemManager itemManager, IBankAccountManager bankAccountManager)
        {
            _unitOfWork = unitOfWork;
            _payableManager = payableManager;
            _itemManager = itemManager;
            _bankAccountManager = bankAccountManager;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateBill()
        {
            var viewModel = new CreatePayableView();

            viewModel.TransactionDate = DateTime.Now.ToString("dd/MM/yyyy");

            viewModel.LineItemList = _itemManager.PayableList();

            viewModel.BankAccountList = _bankAccountManager.GetBankAccounts();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveBill(CreatePayableView viewModel)
        {


            viewModel.LineItemList = _unitOfWork.LineItemsRepository.Items
                .Where(x => x.AccountGroupType == AccountGroupType.Expenditure || x.AccountGroupType == AccountGroupType.Assets)
                .ToList();
            viewModel.BankAccountList = _unitOfWork.BankAccountsRepository.Items.ToList();

            if (ModelState.IsValid)
            {
<<<<<<< HEAD

                var payable = _payableManager.Save(viewModel);

                TempData["AlertMessage"] = $"Your bill was created successfully. Your bill number is BP-{payable.BillNumber}";
=======
                int counter = _unitOfWork.BillPayablesRepository.Items.ToList().Count;

                if (GetLineItemBudget(viewModel.Economic) == 0)
                {
                    ModelState.AddModelError("Economic", "Budget has not yet been configured for this economic item.");

                    return View("CreateBill", viewModel);
                }


                if (!IsBelowBudgetLimit(decimal.Parse(viewModel.Amount), viewModel.Economic))
                {
                    ModelState.AddModelError("Amount", "The Amount entered is above this line item budget.");

                    return View("CreateBill", viewModel);
                }
                
                var payable = new BillPayable()
                {
                    Id = viewModel.Id,
                    PayerId = viewModel.PayerId,
                    Description = viewModel.Description,
                    Organisation = viewModel.Organisation,
                    EconomicId  = viewModel.Economic,
                    GeoCode = viewModel.GeoCode,
                    FundId = viewModel.Fund,
                    Function = viewModel.Function,
                    Quantity = viewModel.Quantity,
                    Rate = viewModel.Rate,
                    Amount = decimal.Parse(viewModel.Amount),
                    TransactionDate = viewModel.TransactionDate,
                    Status = BillStatusType.DRAFT,
                };

                

                int billNumber = ++counter;

                payable.BillNumber = Convert.ToString(billNumber);

                _unitOfWork.BillPayablesRepository.Insert(payable);
                _unitOfWork.SaveChanges();

                TempData["AlertMessage"] = $"Your bill was created successfully. Your bill number is BP {billNumber}";
>>>>>>> master

                return RedirectToAction("Index");
            }

<<<<<<< HEAD
            viewModel.LineItemList = _itemManager.PayableList();

            viewModel.BankAccountList = _bankAccountManager.GetBankAccounts();

=======
>>>>>>> master
            return View("CreateBill", viewModel);
        }

        public IActionResult BillList(string billStatus)
        {

            var viewModel = _payableManager.GetByStatus(billStatus);

            return View(viewModel);
        }

        public IActionResult BillDetail(string billId)
        {
           
            var viewModel = new PayableDetailView
            {
                Payable = _payableManager.GetByGuidId(billId)
            };
            
            return View(viewModel);
        }

        public IActionResult ModifyStatus(PayableDetailView viewModel)
        {

            _payableManager.SetWorkFlowStatus(viewModel);

            TempData["AlertMessage"] = $"Bill was {viewModel.Type.ToString().Replace("_", " ").ToLower()} successfully";

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Checks if line item amout is below
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="lineItemId"></param>
        /// <returns>Status</returns>
        public bool IsBelowBudgetLimit(decimal amount, Guid lineItemId)
        {
            bool status;

            decimal totalPayable = GetTotalPayable(lineItemId);

            decimal lineItemBudget = GetLineItemBudget(lineItemId);

            totalPayable += amount;

            status = totalPayable <= lineItemBudget;

            return status;
        }

        /// <summary>
        /// Get Total Payable
        /// </summary>
        /// <param name="lineItemId"></param>
        /// <returns>Amount</returns>
        public decimal GetTotalPayable(Guid lineItemId)
        {
            var payableListType = _unitOfWork.BillPayablesRepository.Items
                                        .Where(x => x.EconomicId == lineItemId).ToList();

            return payableListType.Sum(payable => payable.Amount);
        }


        /// <summary>
        /// Get Line Item Budget
        /// </summary>
        /// <param name="lineItemId"></param>
        /// <returns>Amount</returns>
        public decimal GetLineItemBudget(Guid lineItemId)
        {
            decimal lineItemBudget = 0;

            var lineItem = _unitOfWork.BudgetsRepository.Items
                                        .FirstOrDefault(x => x.EconomicId == lineItemId);

            if (lineItem != null)
            {
                lineItemBudget = lineItem.Amount;
            }

            return lineItemBudget;
        }


    }
}
