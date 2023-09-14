using System;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
	public interface IAspNetUserDal : IEntityRepository<AspNetUser>
    {
		public Task<UserInfoDto> GetUserPersonalInformation(ApplicationUser user);
        public Task<List<AllUsersDto>> GetAllUsersPaginatedAndFiltered(UserPaginationAndFilterObject paginationAndFilter, float pageResult);
    }
}

