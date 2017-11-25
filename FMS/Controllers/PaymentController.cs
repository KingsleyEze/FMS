using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Extensions;
using FMS.Models.Receipt;
using FMS.Models.Payment;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    [Authorize]
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
        public IActionResult SearchPaymentResult(string startDate, string endDate, string payer, decimal amount)
        {
            var viewModel = new SearchPaymentView();

            var result = _unitOfWork.BillPayablesRepository.Items
                                .WhereIf(!String.IsNullOrEmpty(payer), p => p.PayerId == payer)
                                .WhereIf(amount != 0, p => p.Amount == amount)
                                .WhereIf(startDate != null, 
                                    p => DateTime.ParseExact(p.TransactionDate, @"dd\/MM\/yyyy", null)
                                      >= DateTime.ParseExact(startDate, @"dd\/MM\/yyyy", null))
                                .WhereIf(endDate != null,
                                    p => DateTime.ParseExact(p.TransactionDate, @"dd\/MM\/yyyy", null)
                                         >= DateTime.ParseExact(endDate, @"dd\/MM\/yyyy", null))
                                .ToList();

            viewModel.SearchResult = result.ToList();

            return View(viewModel);
        }
        public IActionResult PaymentDetail(string billNumber)
        {
            if (String.IsNullOrEmpty(billNumber))
                    return RedirectToAction("SearchPayment");

            var payable = _unitOfWork.BillPayablesRepository
                                .Items.FirstOrDefault(b => b.BillNumber == billNumber);

            if (payable == null)
            {
                TempData["SearchNotFound"] = $"Bill Number {billNumber} was not found!";
                return RedirectToAction("SearchPayment");
            }

            var payments = _unitOfWork.PaymentsRepository
                                .Items.Where(p => p.BillPayable.Id == payable.Id).ToList();
            
            var viewModel = new AddPaymentView
            {
                Payable = payable,
                BillNumber = billNumber,
                Payments = payments,
            };

            return View(viewModel);
        }



        [HttpPost]
        public IActionResult AddPayment(AddPaymentView viewModel)
        {
            var payable = _unitOfWork.BillPayablesRepository
                .Items.FirstOrDefault(b => b.BillNumber == viewModel.BillNumber);

            var paymentMadeList = _unitOfWork.PaymentsRepository.Items.Where(x => x.BillPayable.Id == payable.Id).ToList();

            decimal totalPayment = 0;


            foreach (var made in paymentMadeList)
            {
                totalPayment += made.Amount;
            }

            totalPayment += Convert.ToDecimal(viewModel.Amount);

            decimal amountBilled = payable.Amount;

            if (totalPayment > amountBilled)
            {
                TempData["PaymentError"] = "Amount payable cannot be more than outstanding bill.";
                return RedirectToAction("PaymentDetail", new { billNumber = viewModel.BillNumber });
            }

            var payment = new Payment
            {
                TransactionDate = DateTime.Now.ToString("dd/MM/yyyy"),
                Amount = Convert.ToDecimal(viewModel.Amount),
                Description = viewModel.Description,
                BillPayable = payable,
            };

            _unitOfWork.PaymentsRepository.Insert(payment);

            _unitOfWork.SaveChanges();

            return RedirectToAction("PaymentDetail",new {billNumber = viewModel.BillNumber });
        }

    }
}
