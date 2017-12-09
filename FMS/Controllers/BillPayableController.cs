using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Core;
using AutoMapper;
using FMS.Core.Abstract;
using FMS.Models.BillPayable;
using FMS.Core.Model;
using FMS.Extensions;
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
        //private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BillPayableController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            viewModel.LineItemList = _unitOfWork.LineItemsRepository.Items
                                            .Where(x => x.AccountGroupType == AccountGroupType.Expenditure || x.AccountGroupType == AccountGroupType.Assets)
                                            .ToList();

            viewModel.BankAccountList = _unitOfWork.BankAccountsRepository.Items.ToList();

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

                return RedirectToAction("Index");
            }

            return View("CreateBill", viewModel);
        }

        public IActionResult BillList(string billStatus)
        {
            BillStatusType type = BillStatusHelper.GetType(billStatus);

            var viewModel = _unitOfWork.BillPayablesRepository.Items.Where(x => x.Status == type).ToList();

            return View(viewModel);
        }

        public IActionResult BillDetail(string billId)
        {
           
            Guid.TryParse(billId, out var id);

            var viewModel = new PayableDetailView
            {
                Payable = _unitOfWork.BillPayablesRepository
                                        .Items.Include(x => x.Economic).Include(x => x.Fund)
                                        .FirstOrDefault(p => p.Id == id)
            };
            
            return View(viewModel);
        }

        public IActionResult ModifyStatus(PayableDetailView viewModel)
        {

            var payable = _unitOfWork.BillPayablesRepository
                .Items.FirstOrDefault(p => p.Id == viewModel.Payable.Id);

            payable.Status = viewModel.Type;

            _unitOfWork.BillPayablesRepository.Update(payable);

            if (viewModel.Type != BillStatusType.DRAFT)
            {

                var workflow = new PayableWorkFlow
                {
                    BillPayable = payable,
                    Comment = viewModel.Comment,
                    Date = DateTime.Now
                };

                _unitOfWork.PayableWorkFlowsRepository.Insert(workflow);
            }

            _unitOfWork.SaveChanges();

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
