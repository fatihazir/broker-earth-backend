using System;
using System.Security.Claims;
using Business.Abstract;
using Core.DataAccess.Predicate;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
	public class BrokerManager: IBrokerService
	{
        IBrokerDal _brokerDal;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public BrokerManager(IBrokerDal brokerDal, UserManager<ApplicationUser> userManager)
        {
            _brokerDal = brokerDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _userManager = userManager;
        }

        public async Task<IResult> GetBrokerStatisticsByUser()
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            return new SuccessDataResult<BrokerStatisticsDto>(await _brokerDal.GetBrokerStatisticsByUser(user.Id));
        }

        public async Task<IResult> GetAll()
        {
            return new SuccessDataResult<List<Broker>>(await _brokerDal.GetAll());
        }
    }
}

