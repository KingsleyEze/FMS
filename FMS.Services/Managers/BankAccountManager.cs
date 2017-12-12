using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Services.Managers.Abstract;

namespace FMS.Services.Managers
{
    public class BankAccountManager : IBankAccountManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankAccountManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<BankAccount> GetBankAccounts()
        {
           return _unitOfWork.BankAccountsRepository.Items.ToList();
        }
    }
}
