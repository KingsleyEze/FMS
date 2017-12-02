using FMS.Core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FMS.Utilities.Enums;

namespace FMS.Core.Helper
{
    public class DataToSeed
    {
        private static DataToSeed _outputData;
        public static DataToSeed GetData => _outputData ?? (_outputData = new DataToSeed());


        private IList<Bank> _bankList;
        public IList<Bank> Banks
        {
            get
            {
                if (_bankList == null)
                {
                    _bankList = new List<Bank>();
                    var banks = NigerianBanks();
                    for (int i = 0; i < banks.Count; i++)
                    {
                        _bankList.Add(new Bank { Name = banks[i] });
                    }

                }
                return _bankList;
            }

        }

        private IList<State> _statesList;
        public IList<State> States
        {
            get
            {
                if (_statesList == null)
                {
                    _statesList = new List<State>();
                    var states = NigerianStates();
                    for (int i = 0; i < states.Count; i++)
                    {
                        _statesList.Add(new State { CountryId = 1, Name = states[i] });//, Id = i + 1 });
                    }
                }
                return _statesList;
            }
        }


        private IList<Country> _countryList;
        public IList<Country> Countries
        {
            get
            {
                if (_countryList == null)
                {
                    _countryList = new List<Country>();
                    var countries = OurCountries();
                    for (int i = 0; i < countries.Count; i++)
                    {
                        _countryList.Add(new Country { Name = countries[i] });//, Id = i + 1 });
                    }

                }
                return _countryList;
            }
            
        }

      
        
        IList<string> NigerianStates() => new[] {"Abia","Adamawa","Akwa Ibom","Anambra","Bauchi","Bayelsa","Benue","Borno","Cross River","Delta","Ebonyi", "Edo","Ekiti","Enugu","Gombe","Imo","Jigawa",
            "Kaduna","Kano","Katsina","Kebbi","Kogi","Kwara","Lagos","Nassarawa","Niger","Ogun","Ondo","Osun","Oyo","Plateau","Rivers","Sokoto ","Taraba","Yobe","Zamfara","Abuja"};

        IList<string> OurCountries()
        {
            return new[] { "Nigeria", "Non-Nigerian" };
        }

        IList<string> NigerianBanks()
        {
            return new[] { "CENTRAL BANK OF NIGERIA", "ACCESS BANK PLC", "AFRIBANK NIGERIA PLC", "BANK PHB PLC", "DIAMOND BANK PLC", "ECOBANK NIGERIA PLC",
                        "EQUITORIAL TRUST BANK NIG. LTD", "FIDELITY BANK PLC", "FIRST BANK OF NIG. PLC", "FIRST CITY MONUMENT BANK PLC", "FIRSTLAND BANK PLC",
                        "GUARANTY TRUST BANK PLC", "INTERCONTINENTAL BANK PLC", "NIGERIA INTERNATIONAL BANK LTD (CITIGROUP)", "OCEANIC BANK INTERNATIONAL PLC",
                        "SKYE BANK PLC", "SPRING BANK NIGERIA LTD", "STANBIC IBTC BANK PLC", "STANDARD CHATERED BANK NIG. LTD", "STERLING BANK NIG. LTD",
                        "UBA PLC", "UNION BANK NIG. PLC", "UNITY BANK PLC", "WEMA BANK PLC", "ZENITH BANK PLC."};
        }

    

        public IList<AccountGroupModel> AccountGroupList()
        {
            return new List<AccountGroupModel>
            {
                new AccountGroupModel {Code = "01", Description = "Revenue", Type = AccountGroupType.Revenue},
                new AccountGroupModel {Code = "02", Description = "Expenditure", Type = AccountGroupType.Expenditure},
                new AccountGroupModel {Code = "03", Description = "Assets", Type = AccountGroupType.Assets},
                new AccountGroupModel {Code = "04", Description = "Liabilities", Type = AccountGroupType.Liabilities},
            };
        }


        public IList<AccountSubTypeModel> AccountSubTypeList()
        {
            return new List<AccountSubTypeModel>
            {
                new AccountSubTypeModel {Code = "0101",Description = "Budgetary Allocation", Type = AccountGroupType.Revenue},
                new AccountSubTypeModel {Code = "0102",Description = "Retained Internally Generated Revenue", Type = AccountGroupType.Revenue},
                new AccountSubTypeModel {Code = "0103",Description = "Donations", Type = AccountGroupType.Revenue},
                new AccountSubTypeModel {Code = "0104",Description = "Sundry Revenue", Type = AccountGroupType.Revenue},
                new AccountSubTypeModel {Code = "0201",Description = "Administrative Expenses", Type = AccountGroupType.Expenditure},
                new AccountSubTypeModel {Code = "0202",Description = "Finance and Other Charges", Type = AccountGroupType.Expenditure},
                new AccountSubTypeModel {Code = "0203",Description = "Office Running Expenses", Type = AccountGroupType.Expenditure},
                new AccountSubTypeModel {Code = "0204",Description = "Sundry Expenses", Type = AccountGroupType.Expenditure},
                new AccountSubTypeModel {Code = "0301",Description = "Current Assets", Type = AccountGroupType.Assets},
                new AccountSubTypeModel {Code = "0302",Description = "Fixed Assets", Type = AccountGroupType.Assets},
                new AccountSubTypeModel {Code = "0401",Description = "Current Liabilities", Type = AccountGroupType.Liabilities},
                new AccountSubTypeModel {Code = "0402",Description = "Long Term Liablities", Type = AccountGroupType.Liabilities},
                new AccountSubTypeModel {Code = "0403",Description = "Equity", Type = AccountGroupType.Liabilities},
            };
        }


        public IList<LineItemModel> LineItemList()
        {
            return new List<LineItemModel>
            {
                new LineItemModel{Code = "0101001", Description = "Personnel Allocation", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0101002", Description = "Overhead Allocation", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0101003", Description = "Capital Allocatiion", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0101004", Description = "Other Budgetary Allocation", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0102001", Description = "Sale of Drugs", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0102002", Description = "Consultation Fees", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0102003", Description = "Registration Fees", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0102004", Description = "Inspection Fees", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0102005", Description = "School Fees", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0102006", Description = "Other Internally Generated Revenue", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0103001", Description = "Local Donations", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0103002", Description = "Foreign Donations", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0104001", Description = "Investment Income", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0104002", Description = "Sale of Stores", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0104003", Description = "Interest Income", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0104004", Description = "Rent", Type = AccountGroupType.Revenue},
                new LineItemModel{Code = "0201001", Description = "Salaries and Wages", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0201002", Description = "Other Staff Benefits", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0201003", Description = "Office Rent", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0201004", Description = "Training", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0201005", Description = "Power and Energy", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0201006", Description = "Repairs and Maintenance", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0201007", Description = "Printing and Stationaries", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0201008", Description = "Transport and Travelling", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0201009", Description = "Newspapers, Postages and Telephones", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0201010", Description = "Sundry Administrative Expenses", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0202001", Description = "Interest Payment", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0202002", Description = "Bank Charges and Commissions", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0202003", Description = "Bad Debts Written Off", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0202004", Description = "Professional Fees", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0202005", Description = "Audit and Consultancy Fees", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0202006", Description = "Depreciation", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0202007", Description = "Impairment Loss", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0202008", Description = "Other Finance Charges", Type = AccountGroupType.Expenditure},
                new LineItemModel{Code = "0301001", Description = "Cash and Bank Balances", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0301002", Description = "Receivables", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0301003", Description = "Inventories", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0301004", Description = "Other Current Assets", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302001", Description = "Financial Assets", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302002", Description = "Intangible Assets", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302003", Description = "Investment Properties"},
                new LineItemModel{Code = "0302004", Description = "Land", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302005", Description = "Buildings", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302006", Description = "Furniture & Fittings", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302007", Description = "Plant & Machinery", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302008", Description = "Office & Computer Equipment", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302009", Description = "Teaching & Research Equipment", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302010", Description = "Library Books", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302011", Description = "Motor Vehicles", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0302012", Description = "Art Collections", Type = AccountGroupType.Assets},
                new LineItemModel{Code = "0401001", Description = "Payables", Type = AccountGroupType.Liabilities},
                new LineItemModel{Code = "0401002", Description = "Accruals", Type = AccountGroupType.Liabilities},
                new LineItemModel{Code = "0402001", Description = "Loans", Type = AccountGroupType.Liabilities},
                new LineItemModel{Code = "0402002", Description = "Bonds", Type = AccountGroupType.Liabilities},
                new LineItemModel{Code = "0403001", Description = "Accumulated Surplus", Type = AccountGroupType.Liabilities},
            };
        }


        public IList<BankAccount> BankAccountList()
        {
            return new List<BankAccount>
            {
                new BankAccount{ Code = "001", Description = "Main Account"},
                new BankAccount{ Code = "002", Description = "Project Account"},
                new BankAccount{ Code = "003", Description = "Internally Generated Revenue"},
                new BankAccount{ Code = "004", Description = "Public Private Partnership"},
                new BankAccount{ Code = "005", Description = "Sundry Funds"},
            };
        }


        public class AccountGroupModel
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public AccountGroupType Type { get; set; }
        }

        public class AccountSubTypeModel
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public AccountGroupType Type { get; set; }
        }

        public class LineItemModel
        {
            public string Code { get; set; }
            public string Description { get; set; }
            public AccountGroupType Type { get; set; }
        }

        

    }
}
