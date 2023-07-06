using System;
namespace Entities.CustomDataEntryObjects.Load
{
	public class CustomLoadCreateObject
	{
        public int LoadWeight { get; set; }
        public float Commission { get; set; }
        public DateTime LayCanFrom { get; set; }
        public DateTime LayCanTo { get; set; }
        public string Description { get; set; }
        public float Latitude { get; set; }
        public float Longtitude { get; set; }
    }
}

