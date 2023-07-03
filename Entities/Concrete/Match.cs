using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
	public class Match: IEntity
	{
		public int Id { get; set; }
		public int BrokerId { get; set; }
        public string CreatedUserId { get; set; }
        public string EditedUserId { get; set; }
		public int LoadId { get; set; }
		public int ShipId { get; set; }
	}
}

