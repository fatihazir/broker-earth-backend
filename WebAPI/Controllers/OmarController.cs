using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //[Authorize(Roles ="User, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OmarController : ControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("User")]
        public string UserTest()
        {
            return "selamlar user";
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin")]
        public string AdminTest()
        {
            return "selamlar admin";
        }
    }
}
