using System;
using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
	public interface IBrokerDal : IEntityRepository<Broker>
	{
        BrokerStatisticsDto GetBrokerStatisticsByUser(string userId);
    }
}

