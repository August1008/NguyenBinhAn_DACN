using System;

namespace NguyenBinhAn_DACN.Areas.Teachera.Models
{
    public class Pager
    {
        public Pager()
        {
        }

        public Pager(int totalItems, int page, int pageSize)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            TotalItems = totalItems;
            int currentpage = page;
            PageSize = pageSize;

            int startPage = currentpage - 5;
            int endPage = currentpage + 5;

            if (startPage <= 0)
            {
                startPage = 1;
                endPage = endPage - (startPage - 1);
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;

            }
            CurrentPage = currentpage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
