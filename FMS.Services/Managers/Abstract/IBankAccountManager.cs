using System;
using System.Collections.Generic;
using System.Text;
using FMS.Core.Model;

namespace FMS.Services.Managers.Abstract
{
    public interface IBankAccountManager
    {
        List<BankAccount> GetBankAccounts();
    }
}
