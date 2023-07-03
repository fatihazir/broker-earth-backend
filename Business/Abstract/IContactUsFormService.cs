using System;
using Core.DataAccess.PaginationAndFilter;
using Core.Utilities.Results;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;

namespace Business.Abstract
{
	public interface IContactUsFormService
	{
		public Task<IResult> GetContactUsForms(ContactUsFormPaginationAndFilterObject contactUsFormPaginationAndFilterObject);
        public Task<IResult> GetContactUsFormById(int id);
        public Task<IResult> AddContactUsForm(ContactUsForm contactUsForm);
        public Task<IResult> DeleteContactUsForm(int id);
    }
}

