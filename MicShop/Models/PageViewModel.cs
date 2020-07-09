using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
  
namespace MicShop.Models
{
    public class PageViewModel
    {
        public int _pageNumber { get; private set; }
        public int _totalPages { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            _pageNumber = pageNumber;
            _totalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (_pageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (_pageNumber < _totalPages);
            }
        }
    }
}
