using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Models.BillReceivable;
using FMS.Core.Model;
using FMS.Core.Abstract;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    [Authorize]
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

           // ViewBag["billNumber"] = TempData["billNumber"] ?? null;

            var viewModel = new CreateReceivableView();

            viewModel.TransactionDate = DateTime.Now.ToString("dd/MM/yyyy");

            return View(viewModel);
        }

        public IActionResult SaveBill(CreateReceivableView viewModel)
        {
            if (ModelState.IsValid)
            {
                int counter = _unitOfWork.BillReceivablesRepository.Items.ToList().Count;

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
                    Amount = decimal.Parse(viewModel.Amount),
                    TransactionDate = viewModel.TransactionDate
                };

                //Random random = new Random();
                //int randomNumber = random.Next(0, 10000);

                int billNumber = ++counter;

                receivable.BillNumber = Convert.ToString(billNumber);

                _unitOfWork.BillReceivablesRepository.Insert(receivable);
                _unitOfWork.SaveChanges();

                TempData["billNumber"] = billNumber;

                return RedirectToAction("CreateBill");
            }

            return View("CreateBill", viewModel);
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
