using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
	public class Ship:IEntity
	{
        public int Id { get; set; }
        public int? BrokerId { get; set; }
        public string? CreatedUserId { get; set; }
        public string? EditedUserId { get; set; }
        public int? DeadWeight { get; set; }
        public double? ConfidenceInterval { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Name { get; set; }
        public string? Flag { get; set; }
        public string? Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public bool IsDeleted { get; set; }
    }
}

