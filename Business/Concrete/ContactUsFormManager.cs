﻿using System;
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
            contactUsForm.IsDeleted = false;
            contactUsForm.CreatedAt = DateTime.Now;

            _contactUsFormDal.Add(contactUsForm);

            return new SuccessResult("Thank you for your interest. We will back to you soon!");
        }

        public async Task<IResult> DeleteContactUsForm(int id)
        {
            ContactUsForm contactUsFormToBeDeleted = _contactUsFormDal.Get(c => c.Id == id);

            if (contactUsFormToBeDeleted is not null)
            {
                contactUsFormToBeDeleted.IsDeleted = true;
                _contactUsFormDal.Update(contactUsFormToBeDeleted);
                return new SuccessResult("Contact us form deleted");
            }

            return new ErrorResult("Contact us form does not exist.");
        }

        public async Task<IResult> GetContactUsFormById(int id)
        {
            var contactUsForm = _contactUsFormDal.Get(c => c.Id == id);

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
                return new SuccessDataResult<List<ContactUsForm>>(_contactUsFormDal.GetAll(dbFilter));
            }

            var pageResults = 2f;
            var pageCount = Math.Ceiling(_contactUsFormDal.GetCountOfContactUsForms(dbFilter) / pageResults);

            List<ContactUsForm> contactUsForms = _contactUsFormDal.GetContactUsFormsPaginatedAndFiltered(contactUsFormPaginationAndFilterObject, pageResults, dbFilter);
            return new PaginatedSuccessDataResult<List<ContactUsForm>>(contactUsForms, contactUsFormPaginationAndFilterObject.Page, (int)pageCount);
        }
    }
}
