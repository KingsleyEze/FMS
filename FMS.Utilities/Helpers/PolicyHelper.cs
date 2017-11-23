using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FMS.Utilities.Attributes;
using FMS.Utilities.Auth;
using FMS.Utilities.StringKeys;
using Microsoft.AspNetCore.Authorization;

namespace FMS.Utilities.Helpers
{
    public class PolicyHelper
    {
        public static void SetupPolicy(AuthorizationOptions options)
        {
            var policyKeys = GetPolicyKeys();

            foreach (var policyKey in policyKeys)
            {
                var policy = policyKey.GetRawConstantValue().ToString();
                var authRequirement = GetPoliciesWithRequirement.FirstOrDefault(s => s.Name == policy);
                if (authRequirement != null)
                {

                    options.AddPolicy(
                                       policy,
                                       authBuilder =>
                                       {
                                           authBuilder.RequireAuthenticatedUser();
                                       });
                }
                else
                {
                    options.AddPolicy(
                                       policy,
                                       authBuilder =>
                                       {
                                           authBuilder.RequireClaim(policy, PolicyKeys.POLICY_DEFAULT_VALUE);
                                           authBuilder.RequireAuthenticatedUser();
                                       });

                }
            }
        }

        static IList<FieldInfo> GetPolicyKeys()
        {
            var policyKeys = GlobalExtensions.GetConstant(typeof(PolicyKeys));
            var allPolicy = policyKeys.ToList();
            return allPolicy.Where(s => s.GetRawConstantValue().ToString() != PolicyKeys.POLICY_DEFAULT_VALUE).ToList();
        }
        static IList<FieldInfo> GetWebPolicyKeys()
        {
            var policyKeys = GlobalExtensions.GetConstant(typeof(PolicyKeys));
            return policyKeys.Where(s => s.GetRawConstantValue().ToString() != PolicyKeys.POLICY_DEFAULT_VALUE).ToList();
        }

        public static IList<string> GetAllPolicies()
        {
            var policyKeys = GetPolicyKeys();
            var policies = policyKeys.Select(s => s.GetRawConstantValue().ToString()).ToList();
            return policies;
        }
        public static IList<string> GetWebPolicies()
        {
            var policyKeys = GetWebPolicyKeys();
            var policies = policyKeys.Select(s => s.GetRawConstantValue().ToString()).ToList();
            return policies;
        }

        static IList<AuthRequirementModel> policiesWithReq;
        static IList<AuthRequirementModel> GetPoliciesWithRequirement => policiesWithReq ?? (policiesWithReq = PoliciesWithRequirement());
        static IList<AuthRequirementModel> PoliciesWithRequirement()
        {
            var policiesWithReq = (from a in GetPolicyKeys()
                                   let accessAttr = a.GetCustomAttribute<AccessRequirementAttribute>()
                                   where (a.GetCustomAttribute<AccessRequirementAttribute>() != null)
                                   select new AuthRequirementModel
                                   {
                                       Name = a.GetRawConstantValue().ToString(),
                                       Claims = accessAttr.ClaimsRequirement,
                                       AuthRequirement = accessAttr.AuthorizationRequirement,
                                   }).ToList();

            return policiesWithReq;
        }
    }
}
