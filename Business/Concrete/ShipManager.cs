using System;
using System.Linq.Expressions;
using System.Security.Claims;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess.PaginationAndFilter;
using Core.DataAccess.Predicate;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.CustomDataEntryObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class ShipManager : IShipService
    {
        IShipDal _shipDal;
        IBrokerDal _brokerDal;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShipManager(IShipDal shipDal, IBrokerDal brokerDal, UserManager<ApplicationUser> userManager)
        {
            _shipDal = shipDal;
            _brokerDal = brokerDal;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _userManager = userManager;
        }

        public async Task<IResult> GetShipById(int id)
        {
            ApplicationUser? user = await CurrentUser.GetCurrentUser(_userManager);
            Broker? usersBroker = _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            var ship = _shipDal.Get(s => s.Id == id);

            if (ship is not null)
            {
                if (usersBroker.Id == ship.BrokerId)
                {
                    return new SuccessDataResult<Ship>(ship);
                }
                else
                {
                    return new ErrorResult("Since your broker does not own this ship, you are not authorized to see details.");
                }
            }

            return new ErrorResult("Ship does not exist.");
        }

        public async Task<IResult> GetShipsByBroker(ShipPaginationAndFilterObject paginationAndFilter)
        {
            ApplicationUser? user = await CurrentUser.GetCurrentUser(_userManager);

            if (user is null)
                return new ErrorResult("Your broker account does not exist.");

            var dbFilter = PredicateBuilder.True<Ship>();
            dbFilter = dbFilter.And(s => s.CreatedUserId == user.Id);
            dbFilter = dbFilter.And(s => s.IsDeleted == paginationAndFilter.IsDeleted);

            if (paginationAndFilter.All)
            {
                return new SuccessDataResult<List<Ship>>(_shipDal.GetAll(dbFilter));
            }

            var pageResults = 2f;
            var pageCount = Math.Ceiling(_shipDal.GetCountOfShips(dbFilter) / pageResults);

            List<Ship> ships = _shipDal.GetShipsPaginatedAndFiltered(paginationAndFilter, pageResults, dbFilter);
            return new PaginatedSuccessDataResult<List<Ship>>(ships, paginationAndFilter.Page, (int)pageCount);
        }

        [ValidationAspect(typeof(ShipAddValidation))]
        public async Task<IResult> AddShip([FromBody] CustomShipCreateObject customShipCreateObject)
        {
            ApplicationUser? user = await CurrentUser.GetCurrentUser(_userManager);
            Broker? usersBroker = _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            Ship ship = new() {
                BrokerId = usersBroker.Id,
                CreatedUserId = user.Id,
                EditedUserId = user.Id,
                DeadWeight = customShipCreateObject.DeadWeight,
                ConfidenceInterval = customShipCreateObject.ConfidenceInterval,
                AvailableFrom = customShipCreateObject.AvailableFrom,
                CreatedAt = DateTime.Now,
                Name = customShipCreateObject.Name,
                Flag = customShipCreateObject.Flag,
                Description = customShipCreateObject.Description,
                Latitude = customShipCreateObject.Latitude,
                Longtitude = customShipCreateObject.Longtitude,
                IsDeleted = false
            };
            
            return new SuccessDataResult<Ship>(_shipDal.AddAndRetriveData(ship), "Ship created.");
        }

        public async Task<IResult> DeleteShipById(int id)
        {
            ApplicationUser? user = await CurrentUser.GetCurrentUser(_userManager);
            Broker? usersBroker = _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            Ship shipToBeDeleted = _shipDal.Get(s => s.Id == id);

            if(shipToBeDeleted is not null)
            {
                if (usersBroker.Id == shipToBeDeleted.BrokerId)
                {
                    shipToBeDeleted.IsDeleted = true;
                    _shipDal.Update(shipToBeDeleted);
                    return new SuccessResult("Ship deleted");
                }else
                {
                    return new ErrorResult("Since your broker does not own this ship, you are not authorized to delete the ship.");
                }
            }
            

            return new ErrorResult("Ship does not exist.");
        }

        [ValidationAspect(typeof(ShipUpdateValidation))]
        public async Task<IResult> UpdateShip([FromBody] CustomShipUpdateObject customShipUpdateObject, int id)
        {
            ApplicationUser? user = await CurrentUser.GetCurrentUser(_userManager);
            Broker? usersBroker = _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            Ship shipToBeUpdated = _shipDal.Get(s => s.Id == id);

            if (shipToBeUpdated.BrokerId == usersBroker.Id)
            {
                shipToBeUpdated.EditedUserId = user.Id;
                shipToBeUpdated.DeadWeight = customShipUpdateObject.DeadWeight;
                shipToBeUpdated.ConfidenceInterval = customShipUpdateObject.ConfidenceInterval;
                shipToBeUpdated.AvailableFrom = customShipUpdateObject.AvailableFrom;
                shipToBeUpdated.Name = customShipUpdateObject.Name;
                shipToBeUpdated.Flag = customShipUpdateObject.Flag;
                shipToBeUpdated.Description = customShipUpdateObject.Description;
                shipToBeUpdated.Latitude = customShipUpdateObject.Latitude;
                shipToBeUpdated.Longtitude = customShipUpdateObject.Longtitude;

                _shipDal.Update(shipToBeUpdated);
                return new SuccessResult("Ship updated");
            }
            else
            {
                return new ErrorResult("Since your broker does not own this ship, you are not authorized to update.");

            }

            return new ErrorResult("Ship does not exist.");
        }
    }

}

