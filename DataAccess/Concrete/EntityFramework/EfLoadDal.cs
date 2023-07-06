using System;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfLoadDal : EfEntityRepositoryBase<Load, BrokerContext>, ILoadDal
    {
        public int GetCountOfLoads(Expression<Func<Load, bool>> filter)
        {
            using (BrokerContext context = new())
            {
                return context.Loads.Count(filter);
            }
        }

        public List<Load> GetLoadsPaginatedAndFiltered(LoadPaginationAndFilterObject paginationAndFilter, float pageResult, Expression<Func<Load, bool>> filter = null)
        {
            using (BrokerContext context = new())
            {
                var result = context.Loads.Where(filter)
                    .Skip((paginationAndFilter.Page - 1) * (int)pageResult)
                    .Take((int)pageResult).ToList();

                return result;
            }
        }
    }
}

