using Geo.Rest.Domain.Constants;
using Geo.Rest.Domain.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Rest.Domain.Models.Query
{
    public class CollectionQueryParameters : ItemQueryParameters
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

        /// <summary>
        /// A pipe-seperated list of the sort expressions and sort directions (e.g. name:asc|createdAt:desc).
        /// </summary>
        /// <value>
        /// The sort by.
        /// </value>
        public string SortBy { get; set; }


        [BindNever]
        public ICollection<(string SortProperty, string SortDirection)> SortByList => this.SortBy?.Split("|", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(sortBy =>
        {
            var sortByExpression = sortBy.Split(":");

            var sortProperty = sortByExpression.FirstOrDefault();

            if (sortProperty == null)
            {
                return default;
            }

            var sortDirection = sortByExpression.Count() == 2 ? sortByExpression.LastOrDefault() : QueryParameterConstants.SortDirectionAscending;

            return (sortProperty, sortDirection);

        }).Where(sortBy => sortBy != default).ToCollection() ?? new Collection<(string SortProperty, string SortDirection)>();
    }
}
