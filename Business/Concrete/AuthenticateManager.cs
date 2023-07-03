using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Business.ValidationRules.FluentValidation.CustomValidationObjects;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.CustomReturnObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class AuthenticateManager : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        IBrokerDal _brokerDal;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthenticateManager(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IBrokerDal brokerDal)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _brokerDal = brokerDal;
        }

        [ValidationAspect(typeof(LoginValidation))]
        public async Task<IResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = TokenHelpers.CreateToken(authClaims);
                var refreshToken = TokenHelpers.GenerateRefreshToken();

                _ = int.TryParse("7", out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);

                return new SuccessDataResult<CustomLoginReturnObject>(new CustomLoginReturnObject()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken
                });
            }

            return new ErrorResult("Invalid login credentials.");
        }


        [ValidationAspect(typeof(RefreshTokenValidation))]
        public async Task<IResult> RefreshToken(TokenModel tokenModel)
        {
            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = TokenHelpers.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return new ErrorResult("Invalid access token or refresh token");
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Identity.Name;
            //var test = principal.IsInRole("User");
            //var test2 = principal.IsInRole("Admin");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new ErrorResult("Invalid access token or refresh token");
            }

            var newAccessToken = TokenHelpers.CreateToken(principal.Claims.ToList());
            var newRefreshToken = TokenHelpers.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new SuccessDataResult<CustomRefreshTokenReturnObject>(new CustomRefreshTokenReturnObject() {
                Token= new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            });
        }

        [ValidationAspect(typeof(UserRegisterValidation))]
        public async Task<IResult> RegisterUser([FromBody] UserRegisterValidationObject model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new ErrorResult("User already exists.");

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);


            if (!result.Succeeded)
            {
                string errorMessage = string.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessage += error.Description;
                }
                return new ErrorResult(errorMessage);
            }


            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            var registereduser = await _userManager.FindByNameAsync(model.Username);

            _brokerDal.Add(new Broker {BrokerId = registereduser.Id, CompanyName = model.CompanyName });

            return new SuccessResult("User created succesfully.");
        }

        [ValidationAspect(typeof(RegisterValidation))]
        public async Task<IResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new ErrorResult("User already exists.");

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessage += error.Description;
                }
                return new ErrorResult(errorMessage);
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return new SuccessResult("Admin created succesfully.");
        }

        [ValidationAspect(typeof(RegisterValidation))]
        public async Task<IResult> RegisterAssistant([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new ErrorResult("User already exists.");

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessage += error.Description;
                }
                return new ErrorResult(errorMessage);
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Assistant))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Assistant);
            }

            return new SuccessResult("User assistant created succesfully.");
            throw new NotImplementedException();
        }

        public async Task<IResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return new ErrorResult("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return new SuccessResult("Refresh token revoked for " + username + ".");
        }

        public async Task<IResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return new SuccessResult("Refresh token revoked for all users.");
        }
    }
}

