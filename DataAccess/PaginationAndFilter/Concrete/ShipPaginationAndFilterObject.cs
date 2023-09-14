using System;
using Entities.Concrete;

namespace Core.DataAccess.PaginationAndFilter
{
	public class ShipPaginationAndFilterObject: PaginationAndFilterBase<Ship>
	{
        public bool? IsDeleted { get; set; } = false;
    }
}

