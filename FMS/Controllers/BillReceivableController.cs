using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Models.BillReceivable;
using FMS.Core.Model;
using FMS.Core.Abstract;
using FMS.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using FMS.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;

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

            viewModel.LineItemList = _unitOfWork.LineItemsRepository.Items.Where(x => x.Code.StartsWith("01")).ToList();

            viewModel.BankAccountList = _unitOfWork.BankAccountsRepository.Items.ToList();

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
                    EconomicId = viewModel.Economic,
                    GeoCode = viewModel.GeoCode,
                    FundId = viewModel.Fund,
                    Function = viewModel.Function,
                    Quantity = viewModel.Quantity,
                    Rate = viewModel.Rate,
                    Amount = decimal.Parse(viewModel.Amount),
                    TransactionDate = viewModel.TransactionDate,
                    Status = BillStatusType.DRAFT,
                };

                //Random random = new Random();
                //int randomNumber = random.Next(0, 10000);

                int billNumber = ++counter;

                receivable.BillNumber = Convert.ToString(billNumber);

                _unitOfWork.BillReceivablesRepository.Insert(receivable);

                _unitOfWork.SaveChanges();

                TempData["AlertMessage"] = $"Your bill was created successfully. Your bill number is BR {billNumber}";

                return RedirectToAction("Index");
            }

            viewModel.LineItemList = _unitOfWork.LineItemsRepository.Items.Where(x => x.AccountGroupType == AccountGroupType.Revenue).ToList();

            viewModel.BankAccountList = _unitOfWork.BankAccountsRepository.Items.ToList();

            return View("CreateBill", viewModel);
        }

        public IActionResult BillList(string billStatus)
        {
            BillStatusType type = BillStatusHelper.GetType(billStatus);

            var viewModel = _unitOfWork.BillReceivablesRepository.Items.Where(x => x.Status == type).ToList();

            return View(viewModel);
        }

        public IActionResult BillDetail(string billId)
        {
            Guid.TryParse(billId, out var id);

            var viewModel = new ReceivableDetailView
            {
                Receivable = _unitOfWork.BillReceivablesRepository
                                        .Items.Include(x => x.Economic).Include(x => x.Fund)
                                        .FirstOrDefault(p => p.Id == id)
            };

            return View(viewModel);
        }


        public IActionResult ModifyStatus(ReceivableDetailView viewModel)
        {

            var receivable = _unitOfWork.BillReceivablesRepository
                                    .Items.FirstOrDefault(p => p.Id == viewModel.Receivable.Id);

            receivable.Status = viewModel.Type;
            
            _unitOfWork.BillReceivablesRepository.Update(receivable);

            if (viewModel.Type != BillStatusType.DRAFT)
            {

                var workflow = new ReceivableWorkFlow
                {
                    BillReceivable = receivable,
                    Comment = viewModel.Comment,
                    Date = DateTime.Now
                };

                _unitOfWork.ReceivableWorkFlowsRepository.Insert(workflow);
            }

            _unitOfWork.SaveChanges();

            TempData["AlertMessage"] = $"Bill was {viewModel.Type.ToString().Replace("_"," ").ToLower()} successfully";

            return RedirectToAction("Index");
        }


    }
}
