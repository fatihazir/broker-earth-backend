using System;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	public class ContactUsFormController : Controller
	{
		IContactUsFormService _contactUsFormService;

        public ContactUsFormController(IContactUsFormService contactUsFormService)
        {
            _contactUsFormService = contactUsFormService;
        }


    }
}

