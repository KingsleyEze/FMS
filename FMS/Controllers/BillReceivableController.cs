using System;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.Abstract;
using FMS.Core.ViewModel.BillReceivable;
using FMS.Services.Managers.Abstract;
using FMS.Utilities.StringKeys;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    [Authorize]
    public class BillReceivableController : Controller
    {
        private readonly IReceivableManager _receivableManager;
        private readonly ILineItemManager _itemManager;
        private readonly IBankAccountManager _bankAccountManager;

        public BillReceivableController(IReceivableManager receivableManager, ILineItemManager itemManager, 
                            IBankAccountManager bankAccountManager)
        {
            _receivableManager = receivableManager;
            _itemManager = itemManager;
            _bankAccountManager = bankAccountManager;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateBill()
        {

            var viewModel = new CreateReceivableView
            {
                TransactionDate = DateTime.Now.ToString(DateFormatKey.Default),
                LineItemList = _itemManager.ReceivableList(),
                BankAccountList = _bankAccountManager.GetBankAccounts()
            };
            
            return View(viewModel);
        }

        public IActionResult SaveBill(CreateReceivableView viewModel)
        {
            if (ModelState.IsValid)
            {
                var receivable = _receivableManager.Save(viewModel);

                TempData["AlertMessage"] = $"Your bill was created successfully. Your bill number is BR-{receivable.BillNumber}";

                return RedirectToAction("Index");
            }


            viewModel.LineItemList = _itemManager.ReceivableList();

            viewModel.BankAccountList = _bankAccountManager.GetBankAccounts();

            return View("CreateBill", viewModel);
        }

        public IActionResult BillList(string billStatus)
        {
            var viewModel = _receivableManager.GetByStatus(billStatus);

            return View(viewModel);
        }

        public IActionResult BillDetail(string billId)
        {

            var viewModel = new ReceivableDetailView
            {
                Receivable = _receivableManager.GetByGuidId(billId)
            };

            return View(viewModel);
        }


        public IActionResult ModifyStatus(ReceivableDetailView viewModel)
        {

            _receivableManager.SetWorkFlowStatus(viewModel);

            TempData["AlertMessage"] = $"Bill was {viewModel.Type.ToString().Replace("_"," ").ToLower()} successfully";

            return RedirectToAction("Index");
        }


    }
}
