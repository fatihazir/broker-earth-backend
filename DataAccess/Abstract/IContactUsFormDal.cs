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
        int GetCountOfContactUsForms(Expression<Func<ContactUsForm, bool>> filter);
        List<ContactUsForm> GetContactUsFormsPaginatedAndFiltered(ContactUsFormPaginationAndFilterObject contactUsFormPaginationAndFilterObject, float pageResult, Expression<Func<ContactUsForm, bool>> filter = null);
    }
}

