using System;
using Core.Entities.Abstract;
using Core.Entities.Concrete;

namespace Entities.Concrete
{
	public class Broker : IEntity
	{
		public int Id { get; set; }
		public string BrokerId { get; set; }
		public string? AssistantId { get; set; }
		public string CompanyName { get; set; }
	}
}

