using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.CustomDataEntryObjects.Broker;
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
        public async Task<IActionResult> GetStatistics()
        {
            var result = await _brokerService.GetBrokerStatisticsByUser();

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
        [HttpPost]
        public async Task<IActionResult> CreateBroker(CustomBrokerCreateObject customBrokerCreateObject)
        {
            var result = await _brokerService.CreateBrokerAccount(customBrokerCreateObject);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}

