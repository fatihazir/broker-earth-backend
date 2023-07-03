using System;
using Business.ValidationRules.FluentValidation.CustomValidationObjects;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class UserRegisterValidation: AbstractValidator<UserRegisterValidationObject>
	{
		public UserRegisterValidation()
		{
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
            RuleFor(r => r.Username).NotEmpty();
			RuleFor(r => r.CompanyName).NotEmpty();
        }
	}
}

