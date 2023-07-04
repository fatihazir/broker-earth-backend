using System;
using Core.DataAccess.PaginationAndFilter;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.CustomDataEntryObjects;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Business.Abstract
{
	public interface IShipService
	{
		public Task<IResult> GetShipsByBroker(ShipPaginationAndFilterObject paginationAndFilter);
        public Task<IResult> GetShipById(int id);
        public Task<IResult> AddShip([FromBody] CustomShipCreateObject customShipCreateObject);
        public Task<IResult> UpdateShip([FromBody] CustomShipUpdateObject customShipUpdateObject, int shipId);
        public Task<IResult> DeleteShipById(int id);
    }
}

