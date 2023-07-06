using System;
using System.Linq.Expressions;
using Core.DataAccess;
using Core.DataAccess.PaginationAndFilter;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;

namespace DataAccess.Abstract
{
	public interface ILoadDal: IEntityRepository<Load>
	{
        int GetCountOfLoads(Expression<Func<Load, bool>> filter);
        List<Load> GetLoadsPaginatedAndFiltered(LoadPaginationAndFilterObject paginationAndFilter, float pageResult, Expression<Func<Load, bool>> filter = null);
    }
}

