using System;
namespace Core.Utilities.Results
{
	public class PaginatedResultObject<T>
	{
        public T Data { get; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public int TotalEntityAmount { get; set; }

        public PaginatedResultObject(T data, int currentPage, int pages, int totalEntityAmount)
        {
            Data = data;
            CurrentPage = currentPage;
            Pages = pages;
            TotalEntityAmount = totalEntityAmount;
        }

        public PaginatedResultObject(T data, int currentPage, int pages)
        {
            Data = data;
            CurrentPage = currentPage;
            Pages = pages;
            TotalEntityAmount = 0;
        }

        public PaginatedResultObject()
        {

        }
    }
}

