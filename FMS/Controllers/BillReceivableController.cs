using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Models.BillReceivable;
using FMS.Core.Model;
using FMS.Core.Abstract;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    public class BillReceivableController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BillReceivableController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateBill()
        {
            return View();
        }

        public IActionResult SaveBill(CreateReceivableView viewModel)
        {
           

            var receivable = new BillReceivable()
            {
                Id = viewModel.Id,
                PayeeId = viewModel.PayeeId,
                Description = viewModel.Description,
                Organisation = viewModel.Organisation,
                Economic = viewModel.Economic,
                GeoCode = viewModel.GeoCode,
                Fund = viewModel.Fund,
                Function = viewModel.Function,
                Quantity = viewModel.Quantity,
                Rate = viewModel.Rate,
                Amount = viewModel.Amount,
                TransactionDate = viewModel.TransactionDate
            };

            Random random = new Random();
            int randomNumber = random.Next(0, 10000);

            receivable.BillNumber = Convert.ToString(randomNumber);

            _unitOfWork.BillReceivablesRepository.Insert(receivable);
            _unitOfWork.SaveChanges();

            return RedirectToAction("CreateBill");
        }

        public IActionResult ReviewBill()
        {
            return View();
        }

        public IActionResult ApproveBill()
        {
            return View();
        }

        public IActionResult FinalizeBill()
        {
            return View();
        }

    }
}
