using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    
    public class BudgetController : Controller
    {
        public BudgetController()
        {
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult CreateBudget()
        {
            return View();
        }


        public IActionResult LoadBudget()
        {
            return View();
        }

        public IActionResult AmendBudget()
        {
            return View();
        }

    }
}
