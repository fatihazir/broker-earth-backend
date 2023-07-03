using System;
using Core.Entities.Abstract;

namespace Entities.DTOs
{
	public class BrokerStatisticsDto: IDto
	{
        public int BrokerId { get; set; }
		public string CompanyName { get; set; }
		public int TotalLoads { get; set; }
		public int TotalShips { get; set; }
		public int TotalMatches { get; set; }
	}
}

