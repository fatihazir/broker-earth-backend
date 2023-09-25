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
using Entities.CustomDataEntryObjects.Broker;
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

        public async Task<IResult> CreateBrokerAccount(CustomBrokerCreateObject customBrokerCreateObject)
        {
            Broker newBroker = new() {
                BrokerId = customBrokerCreateObject.BrokerId,
                CompanyName = customBrokerCreateObject.CompanyName };

            return new SuccessDataResult<Broker>(await _brokerDal.AddAndRetriveData(newBroker), "A new broker company has initialized.");
        }

        public async Task<IResult> CreateAssistant(CustomAssistantCreateObject customAssistantCreateObject)
        {
            Broker brokerToBeUpdated = await _brokerDal.Get(b => b.Id == customAssistantCreateObject.CompanyId);

            if (brokerToBeUpdated is null)
                return new ErrorResult("Broker company does not exist.");

            var tempAssistantUser = await _userManager.FindByIdAsync(customAssistantCreateObject.AssistantId);

            if (tempAssistantUser is null)
                return new ErrorResult("User does not exist.");

            brokerToBeUpdated.AssistantId = customAssistantCreateObject.AssistantId;

            await _brokerDal.Update(brokerToBeUpdated);

            return new SuccessDataResult<Broker>(brokerToBeUpdated, $"A new assistant has assigned to the broker company named: {brokerToBeUpdated.CompanyName}");
        }
    }
}

