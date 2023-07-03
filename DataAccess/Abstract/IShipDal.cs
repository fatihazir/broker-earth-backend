using System;
using System.Linq.Expressions;
using Core.DataAccess;
using Core.DataAccess.PaginationAndFilter;
using Core.Entities.Abstract;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IShipDal: IEntityRepository<Ship>
    {
		int GetCountOfShips(Expression<Func<Ship, bool>> filter);
        List<Ship> GetShipsPaginatedAndFiltered(ShipPaginationAndFilterObject paginationAndFilter, float pageResult, Expression<Func<Ship, bool>> filter=null);
	}
}

