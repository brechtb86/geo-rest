using Geo.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Domain.Models.Query
{
    public class CollectionQueryParameters
    {
        private int _maxPageSize = QueryParameterConstants.MaxPageSize;
        private int _pageSize = QueryParameterConstants.PageSize;

        public int Page { get; set; } = 1;

        public int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                this._pageSize = (value > this._maxPageSize) ? this._maxPageSize : value;
            }
        }
    }
}
