using System;
using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class RefreshTokenValidation : AbstractValidator<TokenModel>
	{
		public RefreshTokenValidation()
		{
			RuleFor(t => t.AccessToken).NotEmpty();
			RuleFor(t => t.RefreshToken).NotEmpty();
		}
	}
}

