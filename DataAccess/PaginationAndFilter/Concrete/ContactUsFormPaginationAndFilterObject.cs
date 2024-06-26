﻿using System;
using Core.DataAccess.PaginationAndFilter;
using Entities.Concrete;

namespace DataAccess.PaginationAndFilter.Concrete
{
	public class ContactUsFormPaginationAndFilterObject : PaginationAndFilterBase<ContactUsForm>
    {
        public bool? IsDeleted { get; set; } = false;
    }
}

