using Business.Abstract;
using Business.ValidationRules.FluentValidation.CustomValidationObjects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authenticateService.Login(model);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPatch]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            var result = await _authenticateService.RefreshToken(tokenModel);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> Register([FromBody] UserRegisterValidationObject model)
        {
            var result = await _authenticateService.RegisterUser(model);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [Route("register-assistant")]
        public async Task<IActionResult> RegisterAssistant([FromBody] RegisterModel model)
        {
            var result = await _authenticateService.RegisterAssistant(model);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await _authenticateService.RegisterAdmin(model);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpDelete]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var result = await _authenticateService.Revoke(username);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var result = await _authenticateService.RevokeAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        
    }
}
