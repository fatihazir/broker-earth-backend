using System;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class ShipUpdateValidation: AbstractValidator<Ship>
	{
		public ShipUpdateValidation()
		{
            RuleFor(r => r.Id).NotEmpty();
            RuleFor(r => r.DeadWeight).NotEmpty().GreaterThan(0);
            RuleFor(r => r.ConfidenceInterval).NotEmpty().GreaterThan(0);
            RuleFor(r => r.AvailableFrom).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Latitude).NotEmpty();
            RuleFor(r => r.Longtitude).NotEmpty();
        }
	}
}

