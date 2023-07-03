using System;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfBrokerDal : EfEntityRepositoryBase<Broker, BrokerContext>, IBrokerDal
    {
		public BrokerStatisticsDto GetBrokerStatisticsByUser(string userId)
		{
			using(BrokerContext context = new())
			{
				var result = from broker in context.Brokers
							 where (broker.BrokerId == userId ||
                             broker.AssistantId == userId)
							 select new BrokerStatisticsDto
							 {
								 BrokerId = broker.Id,
								 CompanyName = broker.CompanyName,
								 TotalShips = context.Ships.Count(s => s.BrokerId == broker.Id),
								 TotalLoads = context.Loads.Count(c => c.BrokerId == broker.Id),
								 TotalMatches = context.Matches.Count(m => m.BrokerId == broker.Id)
							 };

				return result.FirstOrDefault();
			}
		}
	}
}

