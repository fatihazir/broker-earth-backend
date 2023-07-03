using System;
using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
	public interface IAspNetUserDal : IEntityRepository<AspNetUser>
    {
		public UserInfoDto GetUserPersonalInformation(ApplicationUser user);
	}
}

