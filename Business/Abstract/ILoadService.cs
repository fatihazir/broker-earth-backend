using System;
using Core.DataAccess.PaginationAndFilter;
using Core.Utilities.Results;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.CustomDataEntryObjects;
using Entities.CustomDataEntryObjects.Load;
using Microsoft.AspNetCore.Mvc;

namespace Business.Abstract
{
	public interface ILoadService
	{
        public Task<IResult> GetLoadsByBroker(LoadPaginationAndFilterObject paginationAndFilter);
        public Task<IResult> GetLoadById(int id);
        public Task<IResult> AddLoad([FromBody] CustomLoadCreateObject customLoadCreateObject);
        public Task<IResult> UpdateLoad([FromBody] CustomLoadUpdateObject customLoadUpdateObject, int loadId);
        public Task<IResult> DeleteLoadById(int id);
    }
}

