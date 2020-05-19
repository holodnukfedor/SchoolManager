
using System;

namespace SchoolManagerDAL
{
    public class PaginationParameters
    {
        public const int MinPageSize = 10;

        public int PageNumber { set; get; }
        public int PageSize { set; get; }

        public PaginationParameters(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException($"Page number must be greater then zero. PageNumber: [{pageNumber}]");

            if (pageSize < MinPageSize)
                throw new ArgumentException($"Page size must be greater or equal {MinPageSize}. PageSize: [{pageSize}]");

            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public PaginationParameters()
        {
            PageNumber = 1;
            PageSize = MinPageSize;
        }
    }
}
