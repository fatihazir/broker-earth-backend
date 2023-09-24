using Business.ValidationRules.FluentValidation.CustomValidationObjects;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthenticateService
    {
        public Task<IResult> Login([FromBody] LoginModel model);
        public Task<IResult> RegisterUser([FromBody] UserRegisterValidationObject model);
        //public Task<IResult> RegisterAssistant([FromBody] RegisterModel model);
        //public Task<IResult> RegisterAdmin([FromBody] RegisterModel model);
        public Task<IResult> RefreshToken(TokenModel tokenModel);
        public Task<IResult> Revoke(string username);
        public Task<IResult> RevokeAll();
    }
}
