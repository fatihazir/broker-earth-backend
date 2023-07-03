using System;
using Core.Utilities.Results;

namespace Business.Abstract
{
	public interface IUserService
	{
        public Task<IResult> GetUserPersonalInformation();
    }
}

