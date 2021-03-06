﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FMS.Extensions;
using FMS.Core.Model;
using FMS.Core.Abstract;
using FMS.Core.ViewModel.Account;
using FMS.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //User Types: AppUser, Customer, Staff, Supplier

        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<AppUser> _userManager;

        private SignInManager<AppUser> _signInManager;

        public AccountController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginView());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginView viewModel,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(viewModel.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await _signInManager.PasswordSignInAsync(
                            user, viewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginView.UserName),
                    "Invalid user or password");
            }
            return View(viewModel);
        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login));
        }

        public IActionResult AddAppUser()
        {
            return View(new AppUserView());
        }


        [HttpPost]
        public async Task<IActionResult> SaveAppUser(AppUserView viewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    UserName = viewModel.UserName,
                    Email = viewModel.Email,
                    PhoneNumber = viewModel.PhoneNumber,
                    UserType = UserType.STAFF.ToString()
                };

                IdentityResult result
                    = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View("AddAppUser", viewModel);
        }

        public IActionResult AddStaff()
        {
            var viewModel = new UserDetailView();

            var accountModel = new AccountDetailView();

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            viewModel.UserType = UserType.STAFF;

            viewModel.CountryList = _unitOfWork.CountriesRepository.Items.ToList();

            viewModel.StateList = _unitOfWork.StatesRepository.Items.ToList();

            return View("AddUserDetail", viewModel);
        }

        public IActionResult AddCustomer()
        {
            var viewModel = new UserDetailView();

            var accountModel = new AccountDetailView();

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            viewModel.UserType = UserType.CUSTOMER;

            viewModel.CountryList = _unitOfWork.CountriesRepository.Items.ToList();

            viewModel.StateList = _unitOfWork.StatesRepository.Items.ToList();

            return View("AddUserDetail", viewModel);
        }

        public IActionResult AddSupplier()
        {
            var viewModel = new UserDetailView();

            var accountModel = new AccountDetailView();

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            viewModel.UserType = UserType.SUPPLIER;

            viewModel.CountryList = _unitOfWork.CountriesRepository.Items.ToList();

            viewModel.StateList = _unitOfWork.StatesRepository.Items.ToList();

            return View("AddUserDetail", viewModel);
        }

        public IActionResult AddUserDetail()
        {
            var viewModel = new UserDetailView();

            var accountModel = new AccountDetailView();

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            viewModel.CountryList = _unitOfWork.CountriesRepository.Items.ToList();

            viewModel.StateList = _unitOfWork.StatesRepository.Items.ToList();

            return View(viewModel);
        }
        

        [HttpPost]
        public IActionResult SaveUserDetail(UserDetailView viewModel)
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            if (accountModel.UserDetail == null) accountModel.UserDetail = new UserDetailView();

            accountModel.UserDetail = viewModel;

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            return RedirectToAction("AddBankDetail");
        }

        //Bank Detail
        [HttpGet]
        public IActionResult AddBankDetail()
        {
            var viewModel = new BankDetailView
            {
                BankList = _unitOfWork.BanksRepository.Items.ToList(),
            };
            

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveBankDetail(BankDetailView viewModel)
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            if (accountModel.UserDetail == null) accountModel.BankDetail = new BankDetailView();

            accountModel.BankDetail = viewModel;

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            return RedirectToAction("AddStaffDetail");
        }

        //Staff Detail
        [HttpGet]
        public IActionResult AddStaffDetail()
        {
            var userDetail = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView").UserDetail;
            
            if (UserType.STAFF != userDetail.UserType)
                    return RedirectToAction("AddSupplierDetail");

            var viewModel = new StaffDetailView();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveStaffDetail(StaffDetailView viewModel)
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            if (accountModel.UserDetail == null) accountModel.StaffDetail = new StaffDetailView();

            accountModel.StaffDetail = viewModel;

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            return RedirectToAction("AddSupplierDetail");
        }

        //Supplier Detail
        [HttpGet]
        public IActionResult AddSupplierDetail()
        {
            var userDetail = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView").UserDetail;

            if (UserType.SUPPLIER != userDetail.UserType)
                    return RedirectToAction("Confirmation");

            var viewModel = new SupplierDetailView
            {
                CountryList = _unitOfWork.CountriesRepository.Items.ToList(),
                StateList = _unitOfWork.StatesRepository.Items.ToList()
            };
            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SaveSupplierDetail(SupplierDetailView viewModel)
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            if (accountModel.UserDetail == null) accountModel.SupplierDetail = new SupplierDetailView();

            accountModel.SupplierDetail = viewModel;

            HttpContext.Session.SetObjectAsJson("AccountDetailView", accountModel);

            return RedirectToAction("Confirmation");
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            var accountModel = HttpContext.Session
                    .GetObjectFromJson<AccountDetailView>("AccountDetailView");

            var userDetail = accountModel?.UserDetail;
            var bankDetail = accountModel?.BankDetail;
            var staffDetail = accountModel?.StaffDetail;
            var supplierDetail = accountModel?.SupplierDetail;

            //AppUser Detail
            var appUser = new AppUser
            {
                //Username = userDetail.EmailAddress,
                //Password = "password",
                UserType = userDetail.UserType.ToString(),
                IsActive = true,
            };

            _unitOfWork.AppUsersRepository.Insert(appUser);

            //Profile Detail
            var appUserProfile = new AppUserProfile
            {
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName,
                MiddleName = userDetail.MiddleName,
                Phone = userDetail.Phone,
                EmailAddress = userDetail.EmailAddress,
                Website = userDetail.Website,
                Address = userDetail.Address,
                City = userDetail.City,
                AppUser = appUser,
            };

            _unitOfWork.AppUserProfilesRepository.Insert(appUserProfile);



            //Bank Detail
            if (bankDetail != null)
            {

                var appUserBank = new AppUserBank
                {
                    AccountName = bankDetail.AccountName,
                    AccountNumber = bankDetail.AccountNumber,
                    BVN = bankDetail.BVN,
                    TIN = bankDetail.TIN,
                    AppUser = appUser,
                };

                _unitOfWork.AppUserBanksRepository.Insert(appUserBank);
            }

            //Staff Detail
            if (staffDetail != null)
            {

                var staff = new Staff
                {
                    Title = staffDetail.Title,
                    Rank = staffDetail.Rank,
                    GradeLevel = staffDetail.GradeLevel,
                    DateOfFirstAppoint = staffDetail.DateOfFirstAppoint,
                    DateOfCurrentAppoint = staffDetail.DateOfCurrentAppoint,
                    Notes = staffDetail.Notes,
                    AppUser = appUser,
                };

                _unitOfWork.StaffsRepository.Insert(staff);
            }

            //Supplier Detail
            if (supplierDetail != null)
            {

                var supplier = new Supplier
                {
                    CompanyName = supplierDetail.CompanyName,
                    ContactName = supplierDetail.ContactName,
                    Address = supplierDetail.Address,
                    City = supplierDetail.City,
                    Mobile = supplierDetail.Mobile,
                    OfficePhone = supplierDetail.OfficePhone,
                    Email = supplierDetail.Email,
                    Website = supplierDetail.Website,
                    Notes = supplierDetail.Notes,
                    AppUser = appUser,
                };

                _unitOfWork.SuppliersRepository.Insert(supplier);
            }

            _unitOfWork.SaveChanges();


            return View(accountModel);

        }



    }
}
