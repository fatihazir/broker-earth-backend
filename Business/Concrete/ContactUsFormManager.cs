using System;
using Business.Abstract;
using Business.CCS;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.Predicate;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class ContactUsFormManager : IContactUsFormService
    {
        IContactUsFormDal _contactUsFormDal;

        public ContactUsFormManager(IContactUsFormDal contactUsFormDal)
        {
            _contactUsFormDal = contactUsFormDal;
        }

        [ValidationAspect(typeof(ContactUsFormValidation))]
        public async Task<IResult> AddContactUsForm(ContactUsForm contactUsForm)
        {
            await _contactUsFormDal.Add(contactUsForm);

            return new SuccessResult("Thank you for your interest. We will back to you soon!");
        }

        public async Task<IResult> DeleteContactUsForm(int id)
        {
            ContactUsForm contactUsFormToBeDeleted = await _contactUsFormDal.Get(c => c.Id == id);

            if (contactUsFormToBeDeleted is not null)
            {
                contactUsFormToBeDeleted.IsDeleted = true;
                await _contactUsFormDal.Update(contactUsFormToBeDeleted);
                return new SuccessResult("Contact us form deleted");
            }

            return new ErrorResult("Contact us form does not exist.");
        }

        public async Task<IResult> GetContactUsFormById(int id)
        {
            var contactUsForm = await _contactUsFormDal.Get(c => c.Id == id);

            if(contactUsForm is not null)
            {
                return new SuccessDataResult<ContactUsForm>(contactUsForm);
            }

            return new ErrorResult("Contact us form does not exist.");
        }

        public async Task<IResult> GetContactUsForms(ContactUsFormPaginationAndFilterObject contactUsFormPaginationAndFilterObject)
        {
            var dbFilter = PredicateBuilder.True<ContactUsForm>();
            dbFilter = dbFilter.And(s => s.IsDeleted == contactUsFormPaginationAndFilterObject.IsDeleted);

            if (contactUsFormPaginationAndFilterObject.All)
            {
                return new SuccessDataResult<List<ContactUsForm>>(await _contactUsFormDal.GetAll(dbFilter));
            }

            var pageResults = 5f;
            var pageCount = Math.Ceiling(await _contactUsFormDal.GetCountOfContactUsForms(dbFilter) / pageResults);

            List<ContactUsForm> contactUsForms = await _contactUsFormDal.GetContactUsFormsPaginatedAndFiltered(contactUsFormPaginationAndFilterObject, pageResults, dbFilter);
            return new PaginatedSuccessDataResult<List<ContactUsForm>>(contactUsForms, contactUsFormPaginationAndFilterObject.Page, (int)pageCount);
        }
    }
}

