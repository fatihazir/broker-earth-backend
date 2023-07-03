using System;
using Core.Utilities.Results;

namespace Business.Abstract
{
	public interface IBrokerService
	{
        public Task<IResult> GetBrokerStatisticsByUser();
    }
}

