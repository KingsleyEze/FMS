﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Core.ViewModel.BillPayable;
using FMS.Services.Managers.Abstract;
using FMS.Utilities.Enums;
using FMS.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FMS.Services.Managers
{
    public class PayableManager: IPayableManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public PayableManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public BillPayable GetByGuidId(string billId)
        {
            Guid.TryParse(billId, out var id);

            var payable = _unitOfWork.BillPayablesRepository
                            .Items.Include(x => x.Economic).Include(x => x.Fund)
                            .FirstOrDefault(p => p.Id == id);

            return payable;
        }

        public List<BillPayable> GetByStatus(string status)
        {
            BillStatusType type = BillStatusHelper.GetType(status);

            return _unitOfWork.BillPayablesRepository.Items.Where(x => x.Status == type).ToList();
        }

        public BillPayable Save(CreatePayableView viewModel)
        {
            int counter = _unitOfWork.BillPayablesRepository.Items.ToList().Count;

            var payable = new BillPayable()
            {
                Id = viewModel.Id,
                PayerId = viewModel.PayerId,
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

            payable.BillNumber = Convert.ToString(billNumber);

            _unitOfWork.BillPayablesRepository.Insert(payable);

            _unitOfWork.SaveChanges();

            return payable;
        }

        public BillPayable SetWorkFlowStatus(PayableDetailView viewModel)
        {
            var payable = _unitOfWork.BillPayablesRepository
                .Items.FirstOrDefault(p => p.Id == viewModel.Payable.Id);

            payable.Status = viewModel.Type;

            _unitOfWork.BillPayablesRepository.Update(payable);

            if (viewModel.Type != BillStatusType.DRAFT)
            {

                var workflow = new PayableWorkFlow
                {
                    BillPayable = payable,
                    Comment = viewModel.Comment,
                    Date = DateTime.Now
                };

                _unitOfWork.PayableWorkFlowsRepository.Insert(workflow);
            }

            _unitOfWork.SaveChanges();

            return payable;
        }

        /// <summary>
        /// Checks if line item amout is below
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="lineItemId"></param>
        /// <returns>Status</returns>
        public bool IsBelowBudgetLimit(decimal amount, Guid lineItemId)
        {
            bool status;

            decimal totalPayable = GetTotalPayable(lineItemId);

            decimal lineItemBudget = GetLineItemBudget(lineItemId);

            totalPayable += amount;

            status = totalPayable <= lineItemBudget;

            return status;
        }

        /// <summary>
        /// Get Total Payable
        /// </summary>
        /// <param name="lineItemId"></param>
        /// <returns>Amount</returns>
        public  decimal GetTotalPayable(Guid lineItemId)
        {
            var payableListType = _unitOfWork.BillPayablesRepository.Items
                .Where(x => x.EconomicId == lineItemId).ToList();

            return payableListType.Sum(payable => payable.Amount);
        }


        /// <summary>
        /// Get Line Item Budget
        /// </summary>
        /// <param name="lineItemId"></param>
        /// <returns>Amount</returns>
        public  decimal GetLineItemBudget(Guid lineItemId)
        {
            decimal lineItemBudget = 0;

            var lineItem = _unitOfWork.BudgetsRepository.Items
                .FirstOrDefault(x => x.EconomicId == lineItemId);

            if (lineItem != null)
            {
                lineItemBudget = lineItem.Amount;
            }

            return lineItemBudget;
        }


    }
}
