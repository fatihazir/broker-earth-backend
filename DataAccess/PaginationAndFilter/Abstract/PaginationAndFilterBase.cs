using System;
using Core.Entities.Abstract;

namespace Core.DataAccess.PaginationAndFilter
{
	public class PaginationAndFilterBase<T> where T : class, IEntity, new()
    {
		public int Page { get; set; } = 1;
		public bool? IsDeleted { get; set; } = false;
		public bool All { get; set; } = false;
	}
}

