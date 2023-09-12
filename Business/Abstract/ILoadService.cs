using System;
using Core.DataAccess.PaginationAndFilter;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.CustomDataEntryObjects;
using Entities.CustomDataEntryObjects.Load;
using Microsoft.AspNetCore.Mvc;

namespace Business.Abstract
{
	public interface ILoadService
	{
        public Task<IResult> GetLoadsByBroker(LoadPaginationAndFilterObject paginationAndFilter, ApplicationUser user);
        public Task<IResult> GetLoadById(int id, ApplicationUser user);
        public Task<IResult> AddLoad([FromBody] CustomLoadCreateObject customLoadCreateObject, ApplicationUser user);
        public Task<IResult> UpdateLoad([FromBody] CustomLoadUpdateObject customLoadUpdateObject, int loadId, ApplicationUser user);
        public Task<IResult> DeleteLoadById(int id, ApplicationUser user);
    }
}

