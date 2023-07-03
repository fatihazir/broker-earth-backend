using System;
using System.Data;
using Business.Abstract;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Entities.CustomDataEntryObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/contact-us-form")]
    [ApiController]
    public class ContactUsFormController : Controller
    {
        IContactUsFormService _contactUsFormService;

        public ContactUsFormController(IContactUsFormService contactUsFormService)
        {
            _contactUsFormService = contactUsFormService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateContactUsForm(CustomContactUsFormCreateObject customContactUsFormCreateObject)
        {
            ContactUsForm tempContactUsForm = new() {
                Id = 0,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                NameAndSurname = customContactUsFormCreateObject.NameAndSurname,
                Email = customContactUsFormCreateObject.Email,
                Title = customContactUsFormCreateObject.Title,
                Message = customContactUsFormCreateObject.Message
            };

            var result = await _contactUsFormService.AddContactUsForm(tempContactUsForm);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("/api/contact-us-form/{formId}")]
        public async Task<IActionResult> GetContactUsFormById(int formId)
        {
            var result = await _contactUsFormService.GetContactUsFormById(formId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("/api/contact-us-forms")]
        public async Task<IActionResult> GetContactUsForms([FromQuery] ContactUsFormPaginationAndFilterObject contactUsFormPaginationAndFilter)
        {
            var result = await _contactUsFormService.GetContactUsForms(contactUsFormPaginationAndFilter);

            if (result.Success)
            {
                return Ok(result); 
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("/api/contact-us-form/{formId}")]
        public async Task<IActionResult> DeleteContactUsForm(int formId)
        {
            var result = await _contactUsFormService.DeleteContactUsForm(formId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}

