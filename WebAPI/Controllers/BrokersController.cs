using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.PaginationAndFilter.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BrokersController : Controller
    {

        private readonly IBrokerService _brokerService;

        public BrokersController(IBrokerService brokerService)
        {
            _brokerService = brokerService;
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("statistics")]
        public async Task<IActionResult> Get()
        {
            var result = await _brokerService.GetBrokerStatisticsByUser();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        
    }
}

