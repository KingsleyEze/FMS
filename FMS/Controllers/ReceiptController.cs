using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.Abstract;
using FMS.Models.Receipt;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
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
        public IActionResult SearchReceiptResult(string startDate, string endDate, string payer, string amount)
        {
            var viewModel = new SearchReceiptView();

            var repo = _unitOfWork.BillReceivablesRepository.Items;
            

            viewModel.SearchResult = repo.ToList();

            return View(viewModel);
        }

        public IActionResult ReceiptDetail(string billNumber)
        {

            var receipt = _unitOfWork.BillReceivablesRepository.Items.FirstOrDefault(b => b.BillNumber == billNumber);

            return View(receipt);
        }

    }
}
