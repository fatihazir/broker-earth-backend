using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Business.CCS;
using Core.DataAccess.PaginationAndFilter;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.CustomDataEntryObjects.Load;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoadsController : ControllerBase
    {
        private ILoadService _loadService;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoadsController(ILoadService loadService, UserManager<ApplicationUser> userManager)
        {
            _loadService = loadService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _userManager = userManager;
        }

        [HttpGet]
        [Route("/api/[controller]/{loadId}")]
        public async Task<IActionResult> GetLoadById(int loadId)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _loadService.GetLoadById(loadId, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet]
        [Route("get-loads-by-broker")]
        public async Task<IActionResult> GetLoadsByBroker([FromQuery] LoadPaginationAndFilterObject paginationAndFilter)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _loadService.GetLoadsByBroker(paginationAndFilter, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("/api/[controller]/{loadId}")]
        public async Task<IActionResult> DeleteLoadById(int loadId)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _loadService.DeleteLoadById(loadId, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddLoad([FromBody] CustomLoadCreateObject customLoadCreateObject)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _loadService.AddLoad(customLoadCreateObject, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPatch]
        [Route("/api/[controller]/{loadId}")]
        public async Task<ActionResult> UpdateLoad([FromBody] CustomLoadUpdateObject customloadUpdateObject, int loadId)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _loadService.UpdateLoad(customloadUpdateObject, loadId, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}

