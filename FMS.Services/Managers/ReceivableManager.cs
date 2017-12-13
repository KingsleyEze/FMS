using FMS.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMS.Core.Model;
using FMS.Core.ViewModel.BillReceivable;
using FMS.Services.Managers.Abstract;
using FMS.Utilities.Enums;
using FMS.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FMS.Services.Managers
{
    public class ReceivableManager:IReceivableManager
    {

        private readonly IUnitOfWork _unitOfWork;
        public ReceivableManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public BillReceivable Save(CreateReceivableView viewModel)
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

            return receivable;
        }

        public BillReceivable SetWorkFlowStatus(ReceivableDetailView viewModel)
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

            return receivable;
        }

        public BillReceivable GetByGuidId(string billId)
        {
            Guid.TryParse(billId, out var id);

            return _unitOfWork.BillReceivablesRepository
                            .Items.Include(x => x.Economic).Include(x => x.Fund)
                            .FirstOrDefault(p => p.Id == id);
        }

        public List<BillReceivable> GetByStatus(string status)
        {
            BillStatusType type = BillStatusHelper.GetType(status);

            return _unitOfWork.BillReceivablesRepository.Items.Where(x => x.Status == type).ToList();
        }
    }
}
