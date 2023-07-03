using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
	public class ContactUsForm : IEntity
	{
		public int Id { get; set; }
		public string NameAndSurname { get; set; }
		public string Email { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}

