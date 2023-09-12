using System;
using System.Data;
using System.Security.Claims;
using Business.Abstract;
using Business.Concrete;
using Core.DataAccess.PaginationAndFilter;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Entities.Concrete;
using Entities.CustomDataEntryObjects.Ship;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShipsController: ControllerBase
	{
		private IShipService _shipService;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShipsController(IShipService shipService, UserManager<ApplicationUser> userManager)
        {
            _shipService = shipService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _userManager = userManager;
        }

        [HttpGet]
        [Route("/api/[controller]/{shipId}")]
        public async Task<IActionResult> GetShipById(int shipId)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _shipService.GetShipById(shipId, user);

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
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _shipService.GetShipsByBroker(paginationAndFilter, user);

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
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _shipService.DeleteShipById(shipId, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddShip([FromBody] CustomShipCreateObject customShipCreateObject)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _shipService.AddShip(customShipCreateObject, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPatch]
        [Route("/api/[controller]/{shipId}")]
        public async Task<ActionResult> UpdateShip([FromBody] CustomShipUpdateObject customShipUpdateObject, int shipId)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _shipService.UpdateShip(customShipUpdateObject, shipId, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}

