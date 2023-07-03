using System;
using System.Data;
using Business.Abstract;
using Core.DataAccess.PaginationAndFilter;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShipsController: ControllerBase
	{
		private IShipService _shipService;

        public ShipsController(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet]
        [Route("/api/[controller]/{shipId}")]
        public async Task<IActionResult> GetShipById(int shipId)
        {
            var result = await _shipService.GetShipById(shipId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        
        [HttpGet]
        [Route("get-ships-by-broker")]
        public async Task<IActionResult> GetShipsByBroker([FromQuery] ShipPaginationAndFilterObject paginationAndFilter)
        {
            var result = await _shipService.GetShipsByBroker(paginationAndFilter);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("/api/[controller]/{shipId}")]
        public async Task<IActionResult> DeleteShipById(int shipId)
        {
            var result = await _shipService.DeleteShipById(shipId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddShip([FromQuery] Ship ship)
        {
            var result = await _shipService.AddShip(ship);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPatch]
        [Route("/api/[controller]/{shipId}")]
        public async Task<ActionResult> UpdateShip([FromQuery] Ship ship, int shipId)
        {
            var result = await _shipService.UpdateShip(ship, shipId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}

