using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
	public class AspNetUser: IEntity
	{
		public string Id { get; set; }
		public string UserName { get; set; }
	}
}

