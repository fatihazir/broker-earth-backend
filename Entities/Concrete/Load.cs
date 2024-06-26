﻿using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
	public class Load : IEntity
	{
		public int Id { get; set; }
		public int? BrokerId { get; set; }
		public string? CreatedUserId { get; set; }
		public string? EditedUserId { get; set; }
		public DateTime? LastEditTime { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? LoadWeight { get; set; }
		public double? Commission { get; set; }
		public DateTime? LayCanFrom { get; set; }
		public DateTime? LayCanTo { get; set; }
		public string? Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

