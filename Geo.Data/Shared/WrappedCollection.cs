using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Data.Shared
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

        public WrappedCollection(ICollection<T> items, int count, int page, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = page;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}
