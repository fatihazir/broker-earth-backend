using System;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.PaginationAndFilter;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfShipDal : EfEntityRepositoryBase<Ship, BrokerContext>, IShipDal
    {
        public int GetCountOfShips(Expression<Func<Ship, bool>> filter = null)
        {
            using (BrokerContext context = new())
            {
                return context.Ships.Count(filter);
            }
        }

        public List<Ship> GetShipsPaginatedAndFiltered(ShipPaginationAndFilterObject paginationAndFilter, float pageResult, Expression<Func<Ship, bool>> filter = null)
        {
            using (BrokerContext context = new())
            {
                var result = context.Ships.Where(filter)
                    .Skip((paginationAndFilter.Page - 1) * (int)pageResult)
                    .Take((int)pageResult).ToList();

                return result;
            }
        }
    }
}

