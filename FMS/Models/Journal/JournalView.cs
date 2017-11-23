using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Utilities.Enums;
using Newtonsoft.Json;

namespace FMS.Models.Journal
{
    public class JournalView
    {
        public StepOneView StepOne { get; set; }
        public StepTwoView StepTwo { get; set; }


        public class  StepOneView
        {
            public string Date { get; set; }
            public string Amount { get; set; }
            public string Description { get; set; }
            public string Economic { get; set; }
            public string Fund { get; set; }
        }

        public class StepTwoView
        {
            public string JournalLineItems { get; set; }
            public string DebitLineItem { get; set; }
            public StepOneView StepOne { get; set; }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class JournalListItem
        {
            [JsonProperty(PropertyName = "economic")]
            public string Economic { get; set; }
            [JsonProperty(PropertyName = "fund")]
            public string Fund { get; set; }
            [JsonProperty(PropertyName = "amount")]
            public decimal Amount { get; set; }
            [JsonProperty(PropertyName = "type")]
            public JournalType Type { get; set; }
        }
    }
}
