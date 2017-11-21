using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FMS.Core.Abstract;
using FMS.Core.Model;
using FMS.Utilities.StringKeys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FMS.Core.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly SignInManager<AppUser> _signInManager;
        readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public bool HasAccess(string claim)
        {
            var hasAccess = _contextAccessor?.HttpContext.User.HasClaim(claim, PolicyKeys.POLICY_DEFAULT_VALUE);
            return hasAccess.GetValueOrDefault(false);

        }

        public async Task UpdateUserAccessAsync()
        {
            if (CurrentContext != null)
            {
                var currentuser = await CurrentUserAsync;
                await _signInManager.RefreshSignInAsync(currentuser);
            }
        }

        public Task<AppUser> CurrentUserAsync => _userManager.GetUserAsync(CurrentContext.User);
        public HttpContext CurrentContext => _contextAccessor.HttpContext;
        public async Task<AppUser> GetUserFromClaim(ClaimsPrincipal user) => await _userManager.GetUserAsync(user);
        public string GetClaimValue(string type) => CurrentContext?.User.Claims.SingleOrDefault(s => s.Type == type)?.Value;

  
    }
}
