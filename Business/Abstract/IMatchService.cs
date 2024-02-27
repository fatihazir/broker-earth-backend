using System;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
	public interface IMatchService
	{
		public Task<IResult> GetAvailableLoadsForShip(int shipId, ApplicationUser user);
        public Task<IResult> GetAvailableShipsForLoad(int loadId, ApplicationUser user);

    }
}

