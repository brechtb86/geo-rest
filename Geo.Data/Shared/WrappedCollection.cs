using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Rest.Data.Shared
{
    public class WrappedCollection<T>
    {
        public ICollection<T> Items { get; set; } = new Collection<T>();

        public int CurrentPage { get; private set; }

        public int TotalPages { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public WrappedCollection()
        {

        }

        public WrappedCollection(ICollection<T> items, int totalCount, int currentPage, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}
