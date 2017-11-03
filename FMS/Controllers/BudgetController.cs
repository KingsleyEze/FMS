using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Repository.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    
    public class BudgetController : Controller
    {
        private IBudgetPaymentRepository _repository;
        public BudgetController(IBudgetPaymentRepository repository)
        {
            _repository = repository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_repository.BudgetPayments);
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
            return View(_repository.BudgetPayments);
        }

    }
}
