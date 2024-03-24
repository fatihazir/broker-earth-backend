using System;
using System.Security.Claims;
using Business.Abstract;
using Business.CCS;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.Predicate;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Entities.CustomDataEntryObjects.Load;
using Entities.CustomDataEntryObjects.Ship;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
	public class LoadManager: ILoadService
	{
        ILoadDal _loadDal;
        IBrokerDal _brokerDal;
        private readonly UserManager<ApplicationUser> _userManager;
       

        public LoadManager(ILoadDal loadDal, IBrokerDal brokerDal, UserManager<ApplicationUser> userManager)
        {
            _loadDal = loadDal;
            _brokerDal = brokerDal;
            _userManager = userManager;
        }

        public async Task<IResult> GetLoadById(int id, ApplicationUser user)
        {
            Broker? usersBroker = await _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            var load = await _loadDal.Get(l => l.Id == id);

            if (load is not null)
            {
                if (usersBroker.Id == load.BrokerId)
                {
                    return new SuccessDataResult<Load>(load);
                }
                else
                {
                    return new ErrorResult("Since your broker does not own this load, you are not authorized to see details.");
                }
            }

            return new ErrorResult("Load does not exist.");
        }

        public async Task<IResult> GetLoadsByBroker(LoadPaginationAndFilterObject paginationAndFilter, ApplicationUser user)
        {
            if (user is null)
                return new ErrorResult("Your broker account does not exist.");

            var dbFilter = PredicateBuilder.True<Load>();
            dbFilter = dbFilter.And(s => s.CreatedUserId == user.Id);
            dbFilter = dbFilter.And(s => s.IsDeleted == paginationAndFilter.IsDeleted);

            if (paginationAndFilter.All)
            {
                return new SuccessDataResult<List<Load>>(await _loadDal.GetAll(dbFilter));
            }

            var pageResults = 2f;
            var pageCount = Math.Ceiling(await _loadDal.GetCountOfLoads(dbFilter) / pageResults);

            List<Load> loads = await _loadDal.GetLoadsPaginatedAndFiltered(paginationAndFilter, pageResults, dbFilter);
            var structuredResult = new PaginatedResultObject<List<Load>>(loads, paginationAndFilter.Page, (int)pageCount);
            return new PaginatedSuccessDataResult<PaginatedResultObject<List<Load>>>(structuredResult);
        }

        [ValidationAspect(typeof(LoadAddValidation))]
        public async Task<IResult> AddLoad([FromBody] CustomLoadCreateObject customLoadCreateObject, ApplicationUser user)
        {
            Broker? usersBroker = await _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            Load load = new()
            {
                BrokerId = usersBroker.Id,
                CreatedUserId = user.Id,
                EditedUserId = user.Id,
                LoadWeight = customLoadCreateObject.LoadWeight,
                Commission = customLoadCreateObject.Commission,
                CreatedAt = DateTime.Now,
                LastEditTime = DateTime.Now,
                LayCanFrom = customLoadCreateObject.LayCanFrom,
                LayCanTo = customLoadCreateObject.LayCanTo,
                Description = customLoadCreateObject.Description,
                Latitude = customLoadCreateObject.Latitude,
                Longtitude = customLoadCreateObject.Longtitude,
                IsDeleted = false
            };

            return new SuccessDataResult<Load>(await _loadDal.AddAndRetriveData(load), "Load created.");
        }

        [ValidationAspect(typeof(LoadUpdateValidation))]
        public async Task<IResult> UpdateLoad([FromBody] CustomLoadUpdateObject customLoadUpdateObject, int loadId, ApplicationUser user)
        {
            Broker? usersBroker = await _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            Load loadToBeUpdated = await _loadDal.Get(l => l.Id == loadId);

            if (loadToBeUpdated.BrokerId == usersBroker.Id)
            {
                loadToBeUpdated.EditedUserId = user.Id;
                loadToBeUpdated.LastEditTime = DateTime.Now;
                loadToBeUpdated.LoadWeight = customLoadUpdateObject.LoadWeight;
                loadToBeUpdated.Commission = customLoadUpdateObject.Commission;
                loadToBeUpdated.Latitude = customLoadUpdateObject.Latitude;
                loadToBeUpdated.Longtitude = customLoadUpdateObject.Longtitude;
                loadToBeUpdated.Description = customLoadUpdateObject.Description;
                loadToBeUpdated.Latitude = customLoadUpdateObject.Latitude;
                loadToBeUpdated.Longtitude = customLoadUpdateObject.Longtitude;
                loadToBeUpdated.LayCanFrom = customLoadUpdateObject.LayCanFrom;
                loadToBeUpdated.LayCanTo = customLoadUpdateObject.LayCanTo;

                await _loadDal.Update(loadToBeUpdated);
                return new SuccessResult("Load updated");
            }
            else
            {
                return new ErrorResult("Since your broker does not own this load, you are not authorized to update.");

            }

            return new ErrorResult("Load does not exist.");
        }

        public async Task<IResult> DeleteLoadById(int id, ApplicationUser user)
        {
            Broker? usersBroker = await _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            Load loadToBeDeleted = await _loadDal.Get(l => l.Id == id);

            if (loadToBeDeleted is not null)
            {
                if (usersBroker.Id == loadToBeDeleted.BrokerId)
                {
                    loadToBeDeleted.IsDeleted = true;
                    await _loadDal.Update(loadToBeDeleted);
                    return new SuccessResult("Load deleted");
                }
                else
                {
                    return new ErrorResult("Since your broker does not own this load, you are not authorized to delete the ship.");
                }
            }


            return new ErrorResult("Load does not exist.");
        }

    }
}

