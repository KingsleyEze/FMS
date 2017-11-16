using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FMS.Controllers
{
    public class JournalController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddJournal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StepOne()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveStepOne()
        {
            return RedirectToAction("StepTwo");
        }

        [HttpGet]
        public IActionResult StepTwo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveStepTwo()
        {
            return RedirectToAction("Confirm");
        }

        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }

    }
}
