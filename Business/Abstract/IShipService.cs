using System;
using Core.DataAccess.PaginationAndFilter;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.CustomDataEntryObjects;
using Entities.CustomDataEntryObjects.Ship;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Business.Abstract
{
	public interface IShipService
	{
		public Task<IResult> GetShipsByBroker(ShipPaginationAndFilterObject paginationAndFilter, ApplicationUser user);
        public Task<IResult> GetShipById(int id, ApplicationUser user);
        public Task<IResult> AddShip([FromBody] CustomShipCreateObject customShipCreateObject, ApplicationUser user);
        public Task<IResult> UpdateShip([FromBody] CustomShipUpdateObject customShipUpdateObject, int shipId, ApplicationUser user);
        public Task<IResult> DeleteShipById(int id, ApplicationUser user);
    }
}

