using System;
using System.Linq.Expressions;
using Core.Utilities.Results;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Entities.CustomDataEntryObjects.Broker;

namespace Business.Abstract
{
	public interface IBrokerService
	{
        public Task<IResult> GetBrokerStatisticsByUser();
        public Task<IResult> GetAll();
        public Task<IResult> CreateBrokerAccount(CustomBrokerCreateObject customBrokerCreateObject);
        public Task<IResult> CreateAssistant(CustomAssistantCreateObject customAssistantCreateObject);
    }
}

