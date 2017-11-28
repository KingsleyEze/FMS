using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.Abstract;
using FMS.Extensions;
using FMS.Models.Receipt;
using FMS.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    [Authorize]
    public class ReceiptController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReceiptController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchReceipt()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchReceiptResult(string startDate, string endDate, string payer, decimal amount)
        {
            var viewModel = new SearchReceiptView();

            var result = _unitOfWork.BillReceivablesRepository.Items
                                .Where(p => p.Status == BillStatusType.APPROVED)
                                .WhereIf(!String.IsNullOrEmpty(payer), p => p.PayeeId == payer)
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

        public IActionResult ReceiptDetail(string billNumber)
        {
            if (String.IsNullOrEmpty(billNumber))
                return RedirectToAction("SearchReceipt");

            var receipt = _unitOfWork.BillReceivablesRepository.Items.FirstOrDefault(b => b.BillNumber == billNumber);

            if (receipt == null)
            {
                TempData["SearchNotFound"] = $"Bill Number {billNumber} was not found!";
                return RedirectToAction("SearchReceipt");
            }

            return View(receipt);
        }

    }
}
