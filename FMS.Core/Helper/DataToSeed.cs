using FMS.Core.Model;
using System;
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


    }
}
