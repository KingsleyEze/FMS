using System;
using System.Collections.Generic;
using System.Text;
using FMS.Utilities.StringKeys;

namespace FMS.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class AccessRequirementAttribute : System.Attribute
    {
        public AuthRequirements AuthorizationRequirement { get; }
        public string[] ClaimsRequirement { get; }
        public AccessRequirementAttribute(AuthRequirements authReq, string[] claims)
        {
            AuthorizationRequirement = authReq;
            ClaimsRequirement = claims;
        }

    }
}
