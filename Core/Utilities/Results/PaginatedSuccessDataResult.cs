using System;
namespace Core.Utilities.Results
{
	public class PaginatedSuccessDataResult<T>: PaginatedDataResult<T>
	{
        public PaginatedSuccessDataResult(T data, string message, int currentPage, int pages) : base(data, true, message, currentPage, pages)
        {

        }
        public PaginatedSuccessDataResult(T data, int currentPage, int pages) : base(data, true, currentPage, pages)
        {

        }
        public PaginatedSuccessDataResult(T data, string message) : base(data, true, message)
        {

        }
        public PaginatedSuccessDataResult(T data) : base(data, true)
        {

        }
        public PaginatedSuccessDataResult(string message) : base(default, true, message)
        {

        }
        public PaginatedSuccessDataResult() : base(default, true)
        {

        }
    }
}

