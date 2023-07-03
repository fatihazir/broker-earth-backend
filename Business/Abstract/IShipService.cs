using System;
using Core.DataAccess.PaginationAndFilter;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Business.Abstract
{
	public interface IShipService
	{
		public Task<IResult> GetShipsByBroker(ShipPaginationAndFilterObject paginationAndFilter);
        public Task<IResult> GetShipById(int id);
        public Task<IResult> AddShip(Ship ship);
        public Task<IResult> UpdateShip([FromBody]Ship ship, int shipId);
        public Task<IResult> DeleteShipById(int id);
    }
}

