using System;
using Core.Utilities.Results;
using DataAccess.PaginationAndFilter.Concrete;

namespace Business.Abstract
{
	public interface IUserService
	{
        public Task<IResult> GetUserPersonalInformation();
        public Task<IResult> GetAllUsers(UserPaginationAndFilterObject paginationAndFilter);
    }
}

