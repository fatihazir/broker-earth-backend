using System;
using Core.DataAccess.EntityFramework;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.PaginationAndFilter.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAspNetUserDal : EfEntityRepositoryBase<AspNetUser, BrokerContext>, IAspNetUserDal
    {
        public async Task<List<AllUsersDto>> GetAllUsersPaginatedAndFiltered(UserPaginationAndFilterObject paginationAndFilter, float pageResult)
        {
            using (BrokerContext brokerContext = new())
            {
                using (ApplicationDbContext applicationDbContext = new())
                {
                    var BrokerData = await brokerContext.Brokers.ToListAsync();
                    var UserData = await applicationDbContext.Users.ToListAsync();

                    var UserQuery = UserData.Where(u => u.Email.Contains(paginationAndFilter.SearchQuery) || u.UserName.Contains(paginationAndFilter.SearchQuery));

                    if(paginationAndFilter.All != true)
                    {
                        UserQuery = UserData.Where(u => u.Email.Contains(paginationAndFilter.SearchQuery) || u.UserName.Contains(paginationAndFilter.SearchQuery)).
                            Skip((paginationAndFilter.Page - 1) * (int)pageResult).Take((int)pageResult).ToList();
                    }

                    var UserList = UserQuery.ToList();

                    var BrokersResult = (from tempBroker in BrokerData
                                         join tempUser in UserQuery
                                         on tempBroker.BrokerId equals tempUser.Id
                                         select new AllUsersDto
                                         {
                                             CompanyId = tempBroker.Id,
                                             CompanyName = tempBroker.CompanyName,
                                             UserId = tempBroker.BrokerId,
                                             Email = tempUser.Email,
                                             UserName = tempUser.UserName,
                                             IsAssistant = false
                                         }).ToList();

                    var AssistantsResult = (from tempBroker in BrokerData
                                            join tempUser in UserQuery
                                            on tempBroker.AssistantId equals tempUser.Id
                                            select new AllUsersDto
                                            {
                                                CompanyId = tempBroker.Id,
                                                CompanyName = tempBroker.CompanyName,
                                                UserId = tempBroker.AssistantId,
                                                Email = tempUser.Email,
                                                UserName = tempUser.UserName,
                                                IsAssistant = true
                                            }).ToList();

                    var resultsWithBroker = BrokersResult.Concat(AssistantsResult).ToList();

                    foreach (var user in UserList)
                    {
                        bool allowToAdd = true;

                        foreach (var resultWithBroker in resultsWithBroker)
                        {
                            if(user.Id == resultWithBroker.UserId)
                            {
                                allowToAdd = false;
                                continue;
                            }
                        }

                        if (allowToAdd)
                        {
                            resultsWithBroker.Add(new AllUsersDto
                            {
                                UserId = user.Id,
                                UserName = user.UserName,
                                Email = user.Email
                            });
                        }
                    }
                    
                    return resultsWithBroker;
                }
            }
        }

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

