﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchReceipt()
        {
            return View();
        }

        public IActionResult CreateReceipt()
        {
            return View();
        }

        public IActionResult ReceiptDetails()
        {
            return View();
        }

        public IActionResult ReceiptList()
        {
            return View();
        }
    }
}
