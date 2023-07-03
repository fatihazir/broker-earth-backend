using System;
namespace Business.ValidationRules.FluentValidation.CustomValidationObjects
{
	public class UserRegisterValidationObject
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string CompanyName { get; set; }
	}
}

