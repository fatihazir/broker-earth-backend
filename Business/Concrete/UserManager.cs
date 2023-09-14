using System;
using System.Security.Claims;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
	public class UserManager: IUserService
	{
        IAspNetUserDal _aspNetUserDal;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserManager(IAspNetUserDal asNetUserDal, UserManager<ApplicationUser> userManager)
        {
            _aspNetUserDal = asNetUserDal;
            _userManager = userManager;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }


        public async Task<IResult> GetUserPersonalInformation()
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            return new SuccessDataResult<UserInfoDto>(await _aspNetUserDal.GetUserPersonalInformation(user));
        }

        public async Task<IResult> GetAllUsers(UserPaginationAndFilterObject paginationAndFilter)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_userManager.Users.Count() / pageResults);
            var result = await _aspNetUserDal.GetAllUsersPaginatedAndFiltered(paginationAndFilter, pageResults);
            return new PaginatedSuccessDataResult<List<AllUsersDto>>(result, paginationAndFilter.Page, (int)pageCount);
        }
    }
}

