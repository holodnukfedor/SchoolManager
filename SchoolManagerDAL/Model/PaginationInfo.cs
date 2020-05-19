
namespace SchoolManagerDAL
{
    public class PaginationInfo
    {
        public int PageCount { get; }
        public int PageSize { get; }
        public int PageNumber { get; }

        public PaginationInfo(int pageCount, int pageSize, int pageNumber)
        {
            PageCount = pageCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
