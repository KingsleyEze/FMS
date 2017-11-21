using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Core.Abstract;
using FMS.Core.Model;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BankController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //List<Bank> bankList = new List<Bank>
            //{
            //    new Bank { Name = "GTBANK"},
            //    new Bank { Name = "Access Bank"},
            //    new Bank { Name = "Diamond Bank"},
            //    new Bank { Name = "Zenith Bank"},
            //    new Bank { Name = "Stanbic Bank"},
            //};

            //_unitOfWork.BanksRepository.Insert(bankList);

            //_unitOfWork.SaveChanges();

            return null;
        }
    }
}
