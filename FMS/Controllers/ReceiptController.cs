using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.Abstract;

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

        public IActionResult SearchReceipt()
        {
            return View();
        }

        public IActionResult CreateReceipt()
        {
            return View();
        }

        public IActionResult ReceiptDetail(string receiptId)
        {
            Guid Id = Guid.Parse(receiptId);

            var receipt = _unitOfWork.BillReceivablesRepository.Items.Where(b => b.Id == Id).FirstOrDefault();

            return View(receipt);
        }

        public IActionResult ReceiptList(string billNumber)
        {

            var receiptList = _unitOfWork.BillReceivablesRepository.Items.Where(b => b.BillNumber == billNumber).ToList();

            return View(receiptList);
        }
    }
}
