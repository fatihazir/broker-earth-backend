using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.DataAccess.PaginationAndFilter;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.CustomDataEntryObjects.Load;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoadsController : ControllerBase
    {
        private ILoadService _loadService;

        public LoadsController(ILoadService loadService)
        {
            _loadService = loadService;
        }

        [HttpGet]
        [Route("/api/[controller]/{loadId}")]
        public async Task<IActionResult> GetLoadById(int loadId)
        {
            var result = await _loadService.GetLoadById(loadId);

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
            var result = await _loadService.GetLoadsByBroker(paginationAndFilter);

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
            var result = await _loadService.DeleteLoadById(loadId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddLoad([FromBody] CustomLoadCreateObject customLoadCreateObject)
        {
            var result = await _loadService.AddLoad(customLoadCreateObject);

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
            var result = await _loadService.UpdateLoad(customloadUpdateObject, loadId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}

