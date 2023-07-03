using System;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class RegisterValidation: AbstractValidator<RegisterModel>
	{
		public RegisterValidation()
		{
			RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
            RuleFor(r => r.Username).NotEmpty();
        }
	}
}

