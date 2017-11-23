using System;
using System.Collections.Generic;
using System.Text;
using FMS.Utilities.StringKeys;

namespace FMS.Utilities.Auth
{
    internal class AuthRequirementModel
    {
        public string Name { get; set; }
        public string[] Claims { get; set; }
        public AuthRequirements AuthRequirement { get; set; }
    }
}
