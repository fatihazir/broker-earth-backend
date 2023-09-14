using System;
using Core.Entities.Abstract;

namespace Entities.DTOs
{
	public class AllUsersDto: IDto
	{
        public int CompanyId { get; set; }
        public string UserId { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAssistant { get; set; }
    }
}

