using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.Abstract;

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

        public IActionResult CreatePayment()
        {
            return View();
        }

        public IActionResult PaymentDetail(string paymentId)
        {
            Guid Id = Guid.Parse(paymentId);

            var payment = _unitOfWork.BillPayablesRepository.Items.Where(b => b.Id == Id).FirstOrDefault();

            return View(payment);
        }
        public IActionResult PaymentList(string billNumber)
        {
            var paymentList = _unitOfWork.BillPayablesRepository.Items.Where(b => b.BillNumber == billNumber).ToList();

            return View(paymentList);
        }
    }
}
