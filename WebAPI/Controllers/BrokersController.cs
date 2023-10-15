using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.CustomDataEntryObjects.Broker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BrokersController : Controller
    {

        private readonly IBrokerService _brokerService;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public BrokersController(IBrokerService brokerService, UserManager<ApplicationUser> userManager)
        {
            _brokerService = brokerService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _userManager = userManager;
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _brokerService.GetBrokerStatisticsByUser(user.Id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _brokerService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-broker")]
        public async Task<IActionResult> CreateBroker(CustomBrokerCreateObject customBrokerCreateObject)
        {
            var result = await _brokerService.CreateBrokerAccount(customBrokerCreateObject);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("assign-or-update-assistant")]
        public async Task<IActionResult> AssingAssistant([FromBody] CustomAssistantCreateObject customAssistantCreateObject)
        {
            var result = await _brokerService.CreateAssistant(customAssistantCreateObject);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "User")]
        [HttpPost("assign-or-update-assistant-for-broker-owner")]
        public async Task<IActionResult> AssingAssistantForBrokerOwner([FromBody] CustomAssistantCreateObject customAssistantCreateObject)
        {
            string? username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            ApplicationUser? user = await _userManager.FindByNameAsync(username);

            var result = await _brokerService.CreateAssistantForBrokerOwner(customAssistantCreateObject, user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}

