using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Model;
using Microsoft.AspNetCore.Http;

namespace FMS.Core.Abstract
{
    public interface IUserService
    {
        bool HasAccess(string claim);
        
        Task UpdateUserAccessAsync();

        Task<AppUser> CurrentUserAsync { get; }
        HttpContext CurrentContext { get; }
        Task<AppUser> GetUserFromClaim(ClaimsPrincipal user);
        
        string GetClaimValue(string type);
    }
}
