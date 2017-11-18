using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Extensions;
using FMS.Models.Journal;
using FMS.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FMS.Controllers
{
    public class JournalController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public JournalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        

        [HttpGet]
        public IActionResult StepOne()
        {
            var viewModel = new JournalView.StepOneView();

            viewModel.Date = DateTime.Now.ToString("dd/MM/yyyy");

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveStepOne(JournalView.StepOneView viewModel)
        {
            var journalModel = new JournalView
            {
                StepOne = viewModel
            };

            HttpContext.Session.SetObjectAsJson("JournalView", journalModel);

            return RedirectToAction("StepTwo");
        }

        [HttpGet]
        public IActionResult StepTwo()
        {
            var viewModel = new JournalView.StepTwoView();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveStepTwo(JournalView.StepTwoView viewModel)
        {
            var journalModel = HttpContext.Session
                .GetObjectFromJson<JournalView>("JournalView");

            var journalList = 
                JsonConvert.DeserializeObject
                <List<JournalView.JournalListItem>>(viewModel.JournalLineItems);

            int count = _unitOfWork.JournalsRepository.Items.ToList().Count;

            var journal = new Journal
            {
                Code = ++count,
                Description = journalModel.StepOne.Description,
                Economic = journalModel.StepOne.Economic,
                Fund = journalModel.StepOne.Fund,
            };

            _unitOfWork.JournalsRepository.Insert(journal);

           foreach(var line in journalList)
            {
                var journalListItem = new JournalLineItem
                {
                    Journal = journal,
                    Amount = line.Amount,
                    Type = line.Type    
                };

                _unitOfWork.JournalLineItemsRepository.Insert(journalListItem);
            }

            _unitOfWork.SaveChanges();

            return RedirectToAction("Confirm");
        }

        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }

    }
}
