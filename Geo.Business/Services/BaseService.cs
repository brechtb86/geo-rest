using AutoMapper;
using Geo.Rest.Business.Extensions;
using Geo.Rest.Domain.Constants;
using Geo.Rest.Domain.Exceptions;
using Geo.Rest.Domain.Models.Geo;
using Geo.Rest.Domain.Models.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Geo.Rest.Business.Services
{
    public abstract class BaseService
    {
        private readonly IMapper _mapper;

        public BaseService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        protected void SetCurrentCulture(QueryParameters parameters)
        {
            var language = (parameters.Language ?? "en").ToLower();

            switch (language)
            {
                case "cn": language = "zh"; break;                
            }

            var cultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(culture => string.Equals(culture.TwoLetterISOLanguageName, language, StringComparison.CurrentCultureIgnoreCase));

            if (cultureInfo != null)
            {
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
        }

        protected IQueryable<TEntity> TrySortBy<TModel, TEntity>(IQueryable<TEntity> entities, string sortBy, string sortDirection)
            where TModel : Base
        {
            Expression<Func<TEntity, object>> newSortByExpression = null;

            if (string.IsNullOrEmpty(sortBy) || string.IsNullOrEmpty(sortDirection))
            {
                return entities;
            }

            var sortByProperty = typeof(TModel).GetProperties().FirstOrDefault(property => property.Name.Equals(sortBy, StringComparison.InvariantCultureIgnoreCase));

            if (sortByProperty == null)
            {
                throw new SortException(sortBy,
                    $"There is no property with name '{sortBy}' to sort by, please check the spelling.");
            }

            // Check if property is a simple type
            if (!sortByProperty.PropertyType.IsSimpleType())
            {
                throw new NotImplementedException("Sorting by complex types is not supported yet.");
            }

            var typeMaps = this._mapper.ConfigurationProvider.GetAllTypeMaps().AsQueryable();

            // Get the mapping between our model and entity class with a property of sortBy
            // This should be null if our mapping profiles are done correctly (see unit tests for that)
            var modelTypeMap = typeMaps.Where(t => t.DestinationType == typeof(TModel)).FirstOrDefault(t =>
                t.PropertyMaps.Any(p =>
                    p.IsMapped && p.DestinationMember != null && p.DestinationMember.Name == sortByProperty.Name));

            if (modelTypeMap == null)
            {
                throw new NotImplementedException($"Sorting by '{sortBy}' is not supported yet.");
            }

            var modelPropertyMap = modelTypeMap.PropertyMaps.First(p =>
                p.IsMapped && p.DestinationMember != null && p.DestinationMember.Name == sortByProperty.Name);

            // Get the reverse mapping between our model and entity class with a property of sortBy
            // This should be null if our mapping profiles are done correctly (see unit tests for that)
            var entityTypeMap = typeMaps
                .Where(t => t.SourceType == typeof(TEntity) && t.DestinationType == modelTypeMap.DestinationType)
                .FirstOrDefault(t => t.PropertyMaps.Any(p =>
                    p.IsMapped && p.DestinationMember != null &&
                    p.DestinationMember.Name == modelPropertyMap.DestinationMember.Name));

            if (entityTypeMap == null)
            {
                throw new NotImplementedException($"Sorting by '{sortBy}' is not supported yet.");
            }

            var entityPropertyMap = entityTypeMap.PropertyMaps.First(p =>
                p.IsMapped && p.DestinationMember != null &&
                p.DestinationMember.Name == modelPropertyMap.DestinationMember.Name);

            var originalExpression = entityPropertyMap.CustomMapExpression;

            var originalExpressionBody = originalExpression.Body;

            var originalExpressionParameter = originalExpression.Parameters.First();

            newSortByExpression = Expression.Lambda<Func<TEntity, object>>(originalExpressionBody, originalExpressionParameter);

            var cult = Thread.CurrentThread.CurrentCulture;

            return !string.Equals(sortDirection, QueryParameterConstants.SortDirectionDescending, StringComparison.InvariantCultureIgnoreCase)
            ? entities is IOrderedQueryable<TEntity> orderedEntitiesToOrderBy ? orderedEntitiesToOrderBy.ThenBy(newSortByExpression) : entities.OrderBy(newSortByExpression)
            : entities is IOrderedQueryable<TEntity> orderedEntitiesToOrderByDescending ? orderedEntitiesToOrderByDescending.ThenByDescending(newSortByExpression) : entities.OrderByDescending(newSortByExpression);

        }

        protected IQueryable<TEntity> TryFilter<TModel, TEntity>(IQueryable<TEntity> entities, string filter, string filterValue)
            where TModel : Base
        {
            Expression<Func<TEntity, bool>> newFilterExpression = null;

            if (string.IsNullOrEmpty(filter) || filterValue == null)
            {
                return entities;
            }

            var filterProperty = typeof(TModel).GetProperties().FirstOrDefault(property => property.Name.Equals(filter, StringComparison.InvariantCultureIgnoreCase));

            if (filterProperty == null)
            {
                throw new FilterException(filter,
                    $"There is no property with name '{filter}' to filter by, please check the spelling.");
            }

            // Check if property is a simple type
            if (!filterProperty.PropertyType.IsSimpleType())
            {
                throw new NotImplementedException("Filtering by complex types is not supported yet.");
            }

            var typeMaps = this._mapper.ConfigurationProvider.GetAllTypeMaps().AsQueryable();

            // Get the mapping between our model and entity class with a property of sortBy
            // This should be null if our mapping profiles are done correctly (see unit tests for that)
            var modelTypeMap = typeMaps.Where(t => t.DestinationType == typeof(TModel)).FirstOrDefault(t =>
                t.PropertyMaps.Any(p =>
                    p.IsMapped && p.DestinationMember != null && p.DestinationMember.Name == filterProperty.Name));

            if (modelTypeMap == null)
            {
                throw new NotImplementedException($"Filtering by '{filter}' is not supported yet.");
            }

            var modelPropertyMap = modelTypeMap.PropertyMaps.First(p =>
                p.IsMapped && p.DestinationMember != null && p.DestinationMember.Name == filterProperty.Name);

            // Get the reverse mapping between our model and entity class with a property of sortBy
            // This should be null if our mapping profiles are done correctly (see unit tests for that)
            var entityTypeMap = typeMaps
                .Where(t => t.SourceType == typeof(TEntity) && t.DestinationType == modelTypeMap.DestinationType)
                .FirstOrDefault(t => t.PropertyMaps.Any(p =>
                    p.IsMapped && p.DestinationMember != null &&
                    p.DestinationMember.Name == modelPropertyMap.DestinationMember.Name));

            if (entityTypeMap == null)
            {
                throw new NotImplementedException($"Filtering by '{filter}' is not supported yet.");
            }

            var entityPropertyMap = entityTypeMap.PropertyMaps.First(p =>
                p.IsMapped && p.DestinationMember != null &&
                p.DestinationMember.Name == modelPropertyMap.DestinationMember.Name);

            var originalExpression = entityPropertyMap.CustomMapExpression;          

            var originalExpressionParameter = originalExpression.Parameters.First();
                        
            var filterPropertyExpression = Expression.Property(originalExpressionParameter, entityPropertyMap.SourceMember.Name);

            var filterPropertyExpressionToString = Expression.Call(filterPropertyExpression, typeof(object).GetMethod("ToString", Type.EmptyTypes));

            var filterPropertyExpressionToLower = Expression.Call(filterPropertyExpressionToString, typeof(string).GetMethod("ToLower", Type.EmptyTypes));

            var filterPropertyExpressionContains = Expression.Call(filterPropertyExpressionToLower, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(filterValue, typeof(string)));

            newFilterExpression = Expression.Lambda<Func<TEntity, bool>>(filterPropertyExpressionContains, originalExpressionParameter);

            return entities.Where(newFilterExpression.Compile()).AsQueryable();
        }
    }
}
