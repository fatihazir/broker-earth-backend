using System;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.PaginationAndFilter;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfShipDal : EfEntityRepositoryBase<Ship, BrokerContext>, IShipDal
    {
        public async Task<int> GetCountOfShips(Expression<Func<Ship, bool>> filter = null)
        {
            using (BrokerContext context = new())
            {
                return await context.Ships.CountAsync(filter);
            }
        }

        public async Task<List<Ship>> GetShipsPaginatedAndFiltered(ShipPaginationAndFilterObject paginationAndFilter, float pageResult, Expression<Func<Ship, bool>> filter = null)
        {
            using (BrokerContext context = new())
            {
                var result = await context.Ships.Where(filter)
                    .Skip((paginationAndFilter.Page - 1) * (int)pageResult)
                    .Take((int)pageResult).ToListAsync();

                return result;
            }
        }
    }
}

