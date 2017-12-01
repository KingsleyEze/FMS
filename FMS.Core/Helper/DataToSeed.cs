using FMS.Core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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

        IList<AccountGroup> AccountGroups()
        {
            return new List<AccountGroup>
            {
                new AccountGroup {Code = "01", Description = "Revenue"},
                new AccountGroup {Code = "02", Description = "Expenditure"},
                new AccountGroup {Code = "03", Description = "Assets"},
                new AccountGroup {Code = "04", Description = "Liabilities"},
            };
        }


        IList<AccountSubType> AccountSubTypes()
        {
            return new List<AccountSubType>
            {
                new AccountSubType {Code = "0101",Description = "Budgetary Allocation"},
                new AccountSubType {Code = "0102",Description = "Retained Internally Generated Revenue"},
                new AccountSubType {Code = "0103",Description = "Donations"},
                new AccountSubType {Code = "0104",Description = "Sundry Revenue"},
                new AccountSubType {Code = "0201",Description = "Administrative Expenses"},
                new AccountSubType {Code = "0202",Description = "Finance and Other Charges"},
                new AccountSubType {Code = "0203",Description = "Office Running Expenses"},
                new AccountSubType {Code = "0204",Description = "Sundry Expenses"},
                new AccountSubType {Code = "0301",Description = "Current Assets"},
                new AccountSubType {Code = "0302",Description = "Fixed Assets"},
                new AccountSubType {Code = "0401",Description = "Current Liabilities"},
                new AccountSubType {Code = "0402",Description = "Long Term Liablities"},
                new AccountSubType {Code = "0403",Description = "Equity"},
            };
        }


        IList<LineItem> LineItems()
        {
            return new List<LineItem>
            {
                new LineItem{Code = "0101001", Description = "Personnel Allocation"},
                new LineItem{Code = "0101002", Description = "Overhead Allocation"},
                new LineItem{Code = "0101003", Description = "Capital Allocatiion"},
                new LineItem{Code = "0101004", Description = "Other Budgetary Allocation"},
                new LineItem{Code = "0102001", Description = "Sale of Drugs"},
                new LineItem{Code = "0102002", Description = "Consultation Fees"},
                new LineItem{Code = "0102003", Description = "Registration Fees"},
                new LineItem{Code = "0102004", Description = "Inspection Fees"},
                new LineItem{Code = "0102005", Description = "School Fees"},
                new LineItem{Code = "0102006", Description = "Other Internally Generated Revenue"},
                new LineItem{Code = "0103001", Description = "Local Donations"},
                new LineItem{Code = "0103002", Description = "Foreign Donations"},
                new LineItem{Code = "0104001", Description = "Investment Income"},
                new LineItem{Code = "0104002", Description = "Sale of Stores"},
                new LineItem{Code = "0102002", Description = "Consultation Fees"},
                new LineItem{Code = "0102003", Description = "Registration Fees"},
                new LineItem{Code = "0102004", Description = "Inspection Fees"},
                new LineItem{Code = "0102005", Description = "School Fees"},
                new LineItem{Code = "0102006", Description = "Other Internally Generated Revenue"},
                new LineItem{Code = "0103001", Description = "Local Donations"},
                new LineItem{Code = "0103002", Description = "Foreign Donations"},
                new LineItem{Code = "0104001", Description = "Investment Income"},
                new LineItem{Code = "0104002", Description = "Sale of Stores"},
                new LineItem{Code = "0104003", Description = "Interest Income"},
                new LineItem{Code = "0104004", Description = "Rent"},
                new LineItem{Code = "0201001", Description = "Salaries and Wages"},
                new LineItem{Code = "0201002", Description = "Other Staff Benefits"},
                new LineItem{Code = "0201003", Description = "Office Rent"},
                new LineItem{Code = "0201004", Description = "Training"},
                new LineItem{Code = "0201005", Description = "Power and Energy"},
                new LineItem{Code = "0201006", Description = "Repairs and Maintenance"},
                new LineItem{Code = "0201007", Description = "Printing and Stationaries"},
                new LineItem{Code = "0201008", Description = "Transport and Travelling"},
                new LineItem{Code = "0201009", Description = "Newspapers, Postages and Telephones"},
                new LineItem{Code = "0201010", Description = "Sundry Administrative Expenses"},
                new LineItem{Code = "0202001", Description = "Interest Payment"},
                new LineItem{Code = "0202002", Description = "Bank Charges and Commissions"},
                new LineItem{Code = "0202003", Description = "Bad Debts Written Off"},
                new LineItem{Code = "0202004", Description = "Professional Fees"},
                new LineItem{Code = "0202005", Description = "Audit and Consultancy Fees"},
                new LineItem{Code = "0202006", Description = "Depreciation"},
                new LineItem{Code = "0202007", Description = "Impairment Loss"},
                new LineItem{Code = "0202008", Description = "Other Finance Charges"},
                new LineItem{Code = "0301001", Description = "Cash and Bank Balances"},
                new LineItem{Code = "0301002", Description = "Receivables"},
                new LineItem{Code = "0301003", Description = "Inventories"},
                new LineItem{Code = "0301004", Description = "Other Current Assets"},
                new LineItem{Code = "0302001", Description = "Financial Assets"},
                new LineItem{Code = "0302002", Description = "Intangible Assets"},
                new LineItem{Code = "0302003", Description = "Investment Properties"},
                new LineItem{Code = "0302004", Description = "Land"},
                new LineItem{Code = "0302005", Description = "Buildings"},
                new LineItem{Code = "0302006", Description = "Furniture & Fittings"},
                new LineItem{Code = "0302007", Description = "Plant & Machinery"},
                new LineItem{Code = "0302008", Description = "Office & Computer Equipment"},
                new LineItem{Code = "0302009", Description = "Teaching & Research Equipment"},
                new LineItem{Code = "0302010", Description = "Library Books"},
                new LineItem{Code = "0302011", Description = "Motor Vehicles"},
                new LineItem{Code = "0302012", Description = "Art Collections"},
                new LineItem{Code = "0401001", Description = "Payables"},
                new LineItem{Code = "0401002", Description = "Accruals"},
                new LineItem{Code = "0402001", Description = "Loans"},
                new LineItem{Code = "0402002", Description = "Bonds"},
                new LineItem{Code = "0403001", Description = "Accumulated Surplus"},
            };
        }


        IList<BankAccount> BankAccounts()
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

    }
}
