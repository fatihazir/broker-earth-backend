using System;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
	public class EfBrokerDal : EfEntityRepositoryBase<Broker, BrokerContext>, IBrokerDal
    {
		public async Task<BrokerStatisticsDto> GetBrokerStatisticsByUser(string userId)
		{
            using (BrokerContext context = new BrokerContext())
            {
                Broker tempBroker = await context.Brokers.FirstOrDefaultAsync(b => b.BrokerId == userId || b.AssistantId == userId);
                return new BrokerStatisticsDto
                {
                    BrokerId = tempBroker.Id,
                    CompanyName = tempBroker.CompanyName,
                    TotalShips = await context.Ships.CountAsync(s => s.BrokerId == tempBroker.Id),
                    TotalLoads = await context.Loads.CountAsync(c => c.BrokerId == tempBroker.Id),
                    TotalMatches = await context.Matches.CountAsync(m => m.BrokerId == tempBroker.Id)
                };
            }
		}
	}
}

