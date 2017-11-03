using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    public class PaymentController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchPayment()
        {
            return View();
        }

        public IActionResult CreatePayment()
        {
            return View();
        }

        public IActionResult PaymentDetail()
        {
            return View();
        }
        public IActionResult PaymentList()
        {
            return View();
        }
    }
}
