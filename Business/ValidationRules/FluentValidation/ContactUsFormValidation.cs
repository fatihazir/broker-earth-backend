using System;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class ContactUsFormValidation: AbstractValidator<ContactUsForm>
	{
		public ContactUsFormValidation()
		{
			RuleFor(r => r.NameAndSurname).NotEmpty();
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Title).NotEmpty();
            RuleFor(r => r.Message).NotEmpty();

        }
    }
}

