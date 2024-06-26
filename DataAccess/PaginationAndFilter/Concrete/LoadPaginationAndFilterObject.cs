﻿using System;
using Core.DataAccess.PaginationAndFilter;
using Entities.Concrete;

namespace DataAccess.PaginationAndFilter.Concrete
{
	public class LoadPaginationAndFilterObject : PaginationAndFilterBase<Load>
    {
        public bool? IsDeleted { get; set; } = false;
    }
}

