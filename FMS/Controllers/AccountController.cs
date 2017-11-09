using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Models.Account;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    public class AccountController : Controller
    {
        //User Types: AppUser, Customer, Staff, Supplier


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

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveUserDetail(UserDetailView viewModel)
        {
            return View();
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
            return View(viewModel);
        }

        //Staff Detail
        [HttpGet]
        public IActionResult AddStaffDetail()
        {
            var viewModel = new StaffDetailView();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveStaffDetail(StaffDetailView viewModel)
        {
            return View();
        }

        //Supplier Detail
        [HttpGet]
        public IActionResult AddSupplierDetail()
        {
            var viewModel = new SupplierDetailView();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveSupplierDetail(SupplierDetailView viewModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }

    }
}
