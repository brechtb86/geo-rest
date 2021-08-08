using Geo.Rest.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Models.Query
{
    public class CollectionQueryParameters
    {
        private int _maxPageSize = QueryParameterConstants.MaxPageSize;
        private int _pageSize = QueryParameterConstants.PageSize;

        /// <summary>
        /// The page of the collection, the default value is 1.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; } = 1;

        /// <summary>
        /// The size of the page, the default value is 25, the maximum value is 275.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
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
