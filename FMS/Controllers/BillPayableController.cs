using System;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.ViewModel.BillPayable;
using FMS.Services.Managers.Abstract;
using Microsoft.AspNetCore.Authorization;
using FMS.Utilities.StringKeys;


namespace FMS.Controllers
{

    [Authorize]
    public class BillPayableController : Controller
    {
        private readonly IPayableManager _payableManager;
        private readonly ILineItemManager _itemManager;
        private readonly IBankAccountManager _bankAccountManager;

        public BillPayableController(IPayableManager payableManager, ILineItemManager itemManager, 
                            IBankAccountManager bankAccountManager)
        {
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
            var viewModel = new CreatePayableView
            {
                TransactionDate = DateTime.Now.ToString(DateFormatKey.Default),
                LineItemList = _itemManager.PayableList(),
                BankAccountList = _bankAccountManager.GetBankAccounts()
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveBill(CreatePayableView viewModel)
        {
            
            if (ModelState.IsValid)
            {
                viewModel.LineItemList = _itemManager.PayableList();

                viewModel.BankAccountList = _bankAccountManager.GetBankAccounts();

                if (_payableManager.GetLineItemBudget(viewModel.Economic) == 0)
                {
                    ModelState.AddModelError("Economic", CreatePayableView.EconomicConfigError);

                    return View("CreateBill", viewModel);
                }


                if (!_payableManager.IsBelowBudgetLimit(decimal.Parse(viewModel.Amount), viewModel.Economic))
                {
                    ModelState.AddModelError("Amount", CreatePayableView.AmountLimitError);

                    return View("CreateBill", viewModel);
                }

                
                var payable = _payableManager.Save(viewModel);

                TempData["AlertMessage"] = $"Your bill was created successfully. Your bill number is BP-{payable.BillNumber}";
                  

                return RedirectToAction("Index");
            }
            
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
        
    }
}
