using System;
namespace Entities.DTOs
{
	public class UserInfoDto
	{
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int BrokerId { get; set; }
        public string CompanyName { get; set; }
        public bool IsBroker { get; set; }

    }
}

