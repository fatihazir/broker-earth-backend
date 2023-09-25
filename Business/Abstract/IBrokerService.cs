using System;
using System.Linq.Expressions;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Entities.CustomDataEntryObjects.Broker;

namespace Business.Abstract
{
	public interface IBrokerService
	{
        public Task<IResult> GetBrokerStatisticsByUser(string userId);
        public Task<IResult> GetAll();
        public Task<IResult> CreateBrokerAccount(CustomBrokerCreateObject customBrokerCreateObject);
        public Task<IResult> CreateAssistant(CustomAssistantCreateObject customAssistantCreateObject);
        public Task<IResult> CreateAssistantForBrokerOwner(CustomAssistantCreateObject customAssistantCreateObject, ApplicationUser user);
    }
}

