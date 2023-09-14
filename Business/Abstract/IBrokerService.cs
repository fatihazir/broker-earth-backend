using System;
using System.Linq.Expressions;
using Core.Utilities.Results;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;

namespace Business.Abstract
{
	public interface IBrokerService
	{
        public Task<IResult> GetBrokerStatisticsByUser();
        
    }
}

