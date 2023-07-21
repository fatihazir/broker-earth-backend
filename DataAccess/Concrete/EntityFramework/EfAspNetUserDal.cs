using System;
using Core.DataAccess.EntityFramework;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAspNetUserDal : EfEntityRepositoryBase<AspNetUser, BrokerContext>, IAspNetUserDal
    {
        public async Task<UserInfoDto> GetUserPersonalInformation(ApplicationUser user)
        {
            using (BrokerContext context = new BrokerContext())
            {
                Broker tempBroker = await context.Brokers.FirstOrDefaultAsync(b => b.BrokerId == user.Id || b.AssistantId == user.Id);
                return new UserInfoDto
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    BrokerId = tempBroker.Id,
                    CompanyName = tempBroker.CompanyName,
                    IsBroker = user.Id == tempBroker.BrokerId ? true : false
                };
            }
        }
    }

}

