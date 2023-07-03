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
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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

        [ValidationAspect(typeof(ShipAddValidation))]
        public async Task<IResult> AddShip(Ship ship)
        {
            ApplicationUser? user = await CurrentUser.GetCurrentUser(_userManager);
            Broker? usersBroker = _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            ship.BrokerId = usersBroker.Id;
            ship.CreatedUserId = user.Id;
            ship.EditedUserId = user.Id;
            ship.CreatedAt = DateTime.Now;

            return new SuccessDataResult<Ship>(_shipDal.AddAndRetriveData(ship), "Ship created.");
        }

        public async Task<IResult> DeleteShipById(int id)
        {
            ApplicationUser? user = await CurrentUser.GetCurrentUser(_userManager);
            Broker? usersBroker = _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            Ship shipToBeDeleted = _shipDal.Get(s => s.Id == id);
            shipToBeDeleted.IsDeleted = true;

            if(shipToBeDeleted is not null)
            {
                if (usersBroker.Id == shipToBeDeleted.BrokerId)
                {
                    _shipDal.Update(shipToBeDeleted);
                    return new SuccessResult("Ship deleted");
                }else
                {
                    return new ErrorResult("Since your broker does not own this ship, you are not authorized to delete the ship.");
                }
            }
            

            return new ErrorResult("Ship does not exist.");
        }

        public async Task<IResult> GetShipById(int id)
        {
            ApplicationUser? user = await CurrentUser.GetCurrentUser(_userManager);
            Broker? usersBroker = _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            var ship = _shipDal.Get(s => s.Id == id);

            if(ship is not null)
            {
                if(usersBroker.Id == ship.BrokerId)
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

        [ValidationAspect(typeof(ShipUpdateValidation))]
        public async Task<IResult> UpdateShip([FromBody]Ship ship)
        {
            ApplicationUser? user = await CurrentUser.GetCurrentUser(_userManager);
            Broker? usersBroker = _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            if (ship.BrokerId == usersBroker.Id)
            {
                ship.EditedUserId = user.Id;
                _shipDal.Update(ship);
                return new SuccessResult("Ship updated");
            }
            else
            {
                return new ErrorResult("Since your broker does not own this ship, you are not authorized to update.");

            }

            return new ErrorResult("Ship does not exist.");
        }
    }

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}

