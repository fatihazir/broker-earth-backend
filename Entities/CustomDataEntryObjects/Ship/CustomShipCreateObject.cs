using System;
namespace Entities.CustomDataEntryObjects.Ship
{
	public class CustomShipCreateObject
	{
        public int? DeadWeight { get; set; }
        public double? ConfidenceInterval { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public string? Name { get; set; }
        public string? Flag { get; set; }
        public string? Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
    }
}

