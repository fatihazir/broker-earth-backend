using System;
using Core.DataAccess.PaginationAndFilter;
using Entities.Concrete;

namespace DataAccess.PaginationAndFilter.Concrete
{
	public class UserPaginationAndFilterObject : PaginationAndFilterBase<Broker>
    {
		public string SearchQuery { get; set; } = string.Empty;
	}
}

