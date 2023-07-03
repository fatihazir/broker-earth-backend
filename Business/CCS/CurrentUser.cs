using System;
using System.Security.Claims;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.CCS
{
	public static class CurrentUser
	{
        private static IHttpContextAccessor _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        private static UserManager<ApplicationUser> _userManager;

        public static async Task<ApplicationUser> GetCurrentUser(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            return await _userManager.FindByNameAsync(username);
        }
    }
}

