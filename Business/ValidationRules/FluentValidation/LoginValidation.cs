using System;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class LoginValidation : AbstractValidator<LoginModel>
	{
		public LoginValidation()
		{
			RuleFor(u => u.Username).NotEmpty();
			RuleFor(u => u.Password).NotEmpty();
		}
	}
}

