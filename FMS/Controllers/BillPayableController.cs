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
            if (ModelState.IsValid)
            {

                var payable = _payableManager.Save(viewModel);

                TempData["AlertMessage"] = $"Your bill was created successfully. Your bill number is BP-{payable.BillNumber}";

                return RedirectToAction("Index");
            }

            viewModel.LineItemList = _itemManager.PayableList();

            viewModel.BankAccountList = _bankAccountManager.GetBankAccounts();

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
