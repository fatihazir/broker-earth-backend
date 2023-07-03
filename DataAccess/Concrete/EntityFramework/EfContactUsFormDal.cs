using System;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfContactUsFormDal : EfEntityRepositoryBase<ContactUsForm, BrokerContext>, IContactUsFormDal
    {
        public List<ContactUsForm> GetContactUsFormsPaginatedAndFiltered(ContactUsFormPaginationAndFilterObject contactUsFormPaginationAndFilterObject, float pageResult, Expression<Func<ContactUsForm, bool>> filter = null)
        {
            using (BrokerContext context = new())
            {
                var result = context.ContactUsForms.Where(filter)
                    .Skip((contactUsFormPaginationAndFilterObject.Page - 1) * (int)pageResult)
                    .Take((int)pageResult).ToList();

                return result;
            }
        }

        public int GetCountOfContactUsForms(Expression<Func<ContactUsForm, bool>> filter)
        {
            using (BrokerContext context = new())
            {
                return context.ContactUsForms.Count(filter);
            }
        }
    }
}

