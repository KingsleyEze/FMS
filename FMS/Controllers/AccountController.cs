using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Models.Account;
using FMS.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using FMS.Models.Constants;
using FMS.Core.Model;
using FMS.Core.Abstract;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    public class AccountController : Controller
    {
        //User Types: AppUser, Customer, Staff, Supplier

        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //AppUser Detail
        [HttpGet]
        public IActionResult AddUserDetail()
        {
            var viewModel = new UserDetailView();

            var accountModel = new AccountDetailView();

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveUserDetail(UserDetailView viewModel)
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            if (accountModel.UserDetail == null) accountModel.UserDetail = new UserDetailView();

            accountModel.UserDetail = viewModel;

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            return RedirectToAction("AddBankDetail");
        }

        //Bank Detail
        [HttpGet]
        public IActionResult AddBankDetail()
        {
            var viewModel = new BankDetailView();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveBankDetail(BankDetailView viewModel)
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            if (accountModel.UserDetail == null) accountModel.BankDetail = new BankDetailView();

            accountModel.BankDetail = viewModel;

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            return RedirectToAction("AddStaffDetail");
        }

        //Staff Detail
        [HttpGet]
        public IActionResult AddStaffDetail()
        {
            var userDetail = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView").UserDetail;
            
            if (UserType.STAFF != userDetail.UserType)
                    return RedirectToAction("AddSupplierDetail");

            var viewModel = new StaffDetailView();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveStaffDetail(StaffDetailView viewModel)
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            if (accountModel.UserDetail == null) accountModel.StaffDetail = new StaffDetailView();

            accountModel.StaffDetail = viewModel;

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            return RedirectToAction("AddSupplierDetail");
        }

        //Supplier Detail
        [HttpGet]
        public IActionResult AddSupplierDetail()
        {
            var userDetail = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView").UserDetail;

            if (UserType.SUPPLIER != userDetail.UserType)
                    return RedirectToAction("Confirmation");

            var viewModel = new SupplierDetailView();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveSupplierDetail(SupplierDetailView viewModel)
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            if (accountModel.UserDetail == null) accountModel.SupplierDetail = new SupplierDetailView();

            accountModel.SupplierDetail = viewModel;

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            return RedirectToAction("Confirmation");
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            var userDetail = accountModel?.UserDetail;
            var bankDetail = accountModel?.BankDetail;
            var staffDetail = accountModel?.StaffDetail;
            var supplierDetail = accountModel?.SupplierDetail;

            var appUser = new AppUser
            {
                Username = userDetail.EmailAddress,
                Password = "password"
            };

            //_unitOfWork.App

           

            return View(accountModel);
        }

    }
}
