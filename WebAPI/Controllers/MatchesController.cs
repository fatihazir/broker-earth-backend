using System;
using System.Security.Claims;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController: ControllerBase
	{
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private IMatchService _matchService;

        public MatchesController(UserManager<ApplicationUser> userManager, IMatchService matchService)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _userManager = userManager;
            _matchService = matchService;
        }

        [HttpGet]
        [Route("get-available-loads-for-ship/{shipId}")]
        public async Task<IActionResult> GetAvailableLoadsForShip(int shipId)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _matchService.GetAvailableLoadsForShip(shipId, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("get-available-ships-for-load/{loadId}")]
        public async Task<IActionResult> GetAvailableShipsForLoad(int loadId)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _matchService.GetAvailableShipsForLoad(loadId, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}

