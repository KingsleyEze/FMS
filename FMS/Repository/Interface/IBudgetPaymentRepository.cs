using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Models;

namespace FMS.Repository.Interface
{
    public interface IBudgetPaymentRepository
    {
        IQueryable<BudgetPayment> BudgetPayments { get; }
    }
}
