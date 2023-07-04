using System;
using Entities.Concrete;
using Entities.CustomDataEntryObjects;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class ShipUpdateValidation: AbstractValidator<CustomShipUpdateObject>
	{
		public ShipUpdateValidation()
		{
            RuleFor(r => r.DeadWeight).NotEmpty().GreaterThan(0);
            RuleFor(r => r.ConfidenceInterval).NotEmpty().GreaterThan(0);
            RuleFor(r => r.AvailableFrom).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Flag).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Latitude).NotEmpty();
            RuleFor(r => r.Longtitude).NotEmpty();
        }
	}
}

