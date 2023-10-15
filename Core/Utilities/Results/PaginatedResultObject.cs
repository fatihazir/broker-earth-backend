using System;
namespace Core.Utilities.Results
{
	public class PaginatedResultObject<T>
	{
        public T Data { get; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public int EntityAmount { get; set; }

        public PaginatedResultObject(T data, int currentPage, int pages, int entityAmount)
        {
            Data = data;
            CurrentPage = currentPage;
            Pages = pages;
            EntityAmount = entityAmount;
        }

        public PaginatedResultObject(T data, int currentPage, int pages)
        {
            Data = data;
            CurrentPage = currentPage;
            Pages = pages;
            EntityAmount = 0;
        }

        public PaginatedResultObject()
        {

        }
    }
}

