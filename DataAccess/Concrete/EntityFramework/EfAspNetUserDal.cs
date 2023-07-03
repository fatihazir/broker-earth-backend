using System;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAspNetUserDal : EfEntityRepositoryBase<AspNetUser, BrokerContext>, IAspNetUserDal
    {
        public UserInfoDto GetUserPersonalInformation(ApplicationUser user)
        {
            using (BrokerContext context = new())
            {
                var result = from broker in context.Brokers
                             where (broker.BrokerId == user.Id ||
                             broker.AssistantId == user.Id)
                             select new UserInfoDto
                             {
                                 UserId = user.Id,
                                 Username = user.UserName,
                                 Email = user.Email,
                                 BrokerId = broker.Id,
                                 CompanyName = broker.CompanyName,
                                 IsBroker = user.Id == broker.BrokerId ? true : false
                             };

                return result.FirstOrDefault();
            }
        }
    }
}

