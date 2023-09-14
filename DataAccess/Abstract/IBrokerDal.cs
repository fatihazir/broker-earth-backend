using System;
using System.Linq.Expressions;
using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
	public interface IBrokerDal : IEntityRepository<Broker>
	{
        Task<BrokerStatisticsDto> GetBrokerStatisticsByUser(string userId);
    }
}

