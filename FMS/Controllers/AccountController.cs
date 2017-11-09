using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }

        [HttpPost]
        public IActionResult SaveUserDetail()
        {
            return View();
        }

        //Bank Detail
        [HttpGet]
        public IActionResult AddBankDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveBankDetail()
        {
            return View();
        }

        //Staff Detail
        [HttpGet]
        public IActionResult AddStaffDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveStaffDetail()
        {
            return View();
        }

        //Supplier Detail
        [HttpGet]
        public IActionResult AddSupplierDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveSupplierDetail()
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
