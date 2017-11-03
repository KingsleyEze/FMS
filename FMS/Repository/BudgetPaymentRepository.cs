using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Models;
using FMS.Repository.Interface;

namespace FMS.Repository
{
    public class BudgetPaymentRepository : IBudgetPaymentRepository
    {
        public IQueryable<BudgetPayment> BudgetPayments => new List<BudgetPayment> {
                                new BudgetPayment { Name = "Football", Price = 25 },
                                new BudgetPayment { Name = "Surf board", Price = 179 },
                                new BudgetPayment { Name = "Running shoes", Price = 95 }
                                }
        .AsQueryable<BudgetPayment>();
    }
}
