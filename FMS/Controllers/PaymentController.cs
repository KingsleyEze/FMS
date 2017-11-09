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
        public IActionResult SearchPaymentResult(string date = null, string payer = null, string amount = null)
        {
            var viewModel = new SearchPaymentView();

            var repo = _unitOfWork.BillPayablesRepository.Items;

            if (date != null)
                repo.Where(b => b.TransactionDate.Contains(date));
            if (payer != null)
                repo.Where(b => b.PayerId.Contains(payer));
            if (amount != null)
                repo.Where(b => b.Amount.Contains(amount));

            viewModel.SearchResult = repo.ToList();

            return View(viewModel);
        }
        public IActionResult PaymentDetail(string billNumber)
        {

            var payment = _unitOfWork.BillPayablesRepository.Items.Where(b => b.BillNumber == billNumber).FirstOrDefault();

            return View(payment);
        }
        
    }
}
