using System;
using System.Linq.Expressions;
using Core.DataAccess;
using Core.DataAccess.PaginationAndFilter;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;

namespace DataAccess.Abstract
{
	public interface IContactUsFormDal: IEntityRepository<ContactUsForm>
	{
        Task<int> GetCountOfContactUsForms(Expression<Func<ContactUsForm, bool>> filter);
        Task<List<ContactUsForm>> GetContactUsFormsPaginatedAndFiltered(ContactUsFormPaginationAndFilterObject contactUsFormPaginationAndFilterObject, float pageResult, Expression<Func<ContactUsForm, bool>> filter = null);
    }
}

