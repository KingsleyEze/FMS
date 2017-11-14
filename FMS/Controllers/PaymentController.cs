using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.Abstract;
using FMS.Models.Receipt;
using FMS.Models.Payment;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchPayment()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchPaymentResult(string date, string payer, string amount)
        {
            var viewModel = new SearchPaymentView();

            //var repo = _unitOfWork.BillPayablesRepository.Items;

            var result = from s in _unitOfWork.BillPayablesRepository.Items
                            where (date == null || s.TransactionDate == date)
                                    //&& (payer == null || s.PayerId == payer)
                                    //&& (amount == null || s.PayerId == amount)
                            select s;

            
            
            viewModel.SearchResult = result.ToList();

            return View(viewModel);
        }
        public IActionResult PaymentDetail(string billNumber)
        {

            var payment = _unitOfWork.BillPayablesRepository.Items.FirstOrDefault(b => b.BillNumber == billNumber);

            return View(payment);
        }
        
    }
}
