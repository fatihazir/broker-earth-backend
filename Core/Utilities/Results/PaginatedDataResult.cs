using System;
namespace Core.Utilities.Results
{
	public class PaginatedDataResult<T> : Result, IDataResult<T>
    {
        public PaginatedDataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public PaginatedDataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        //public PaginatedDataResult(T data, bool success, string message, int currentPage, int pages) : base(success, message)
        //{
        //    Data = data;
        //    CurrentPage = currentPage;
        //    Pages = pages;
        //}

        //public PaginatedDataResult(T data, bool success, string message, int currentPage, int pages, int entityAmount) : base(success, message)
        //{
        //    Data = data;
        //    CurrentPage = currentPage;
        //    Pages = pages;
        //}

        //public PaginatedDataResult(T data, bool success, int currentPage, int pages) : base(success)
        //{
        //    Data = data;
        //    CurrentPage = currentPage;
        //    Pages = pages;
        //}

        //public PaginatedDataResult(T data, bool success, int currentPage, int pages, int entityAmount) : base(success)
        //{
        //    Data = data;
        //    CurrentPage = currentPage;
        //    Pages = pages;
        //    EntityAmount = entityAmount;
        //}

        public T Data { get; }
        //public int CurrentPage { get; set; }
        //public int Pages { get; set; }
        //public int EntityAmount { get; set; }
    }
}

