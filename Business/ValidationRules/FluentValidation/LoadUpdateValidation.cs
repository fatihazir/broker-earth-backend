using System;
using Entities.CustomDataEntryObjects.Load;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class LoadUpdateValidation : AbstractValidator<CustomLoadUpdateObject>
    {
		public LoadUpdateValidation()
		{
            RuleFor(r => r.LoadWeight).NotEmpty().GreaterThan(0);
            RuleFor(r => r.Commission).NotEmpty();
            RuleFor(r => r.LayCanFrom).NotEmpty();
            RuleFor(r => r.LayCanTo).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Latitude).NotEmpty();
            RuleFor(r => r.Longtitude).NotEmpty();
        }
	}
}

