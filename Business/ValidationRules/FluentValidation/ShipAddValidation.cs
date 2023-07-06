using System;
using Entities.Concrete;
using Entities.CustomDataEntryObjects;
using Entities.CustomDataEntryObjects.Ship;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class ShipAddValidation: AbstractValidator<CustomShipCreateObject>
	{
		public ShipAddValidation()
		{
			RuleFor(r => r.DeadWeight).NotEmpty().GreaterThan(0);
			RuleFor(r => r.ConfidenceInterval).NotEmpty();
			RuleFor(r => r.AvailableFrom).NotEmpty();
			RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Flag).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Latitude).NotEmpty();
			RuleFor(r => r.Longtitude).NotEmpty();
		}
	}
}

