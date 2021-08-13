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
            var language = parameters.Language?.ToLower();

            if (!new[] { "br", "cn", "de", "en", "es", "fa", "fr", "hr", "it", "ja", "kr", "nl", "pt", "zh" }.Contains(language))
            {
                language = "en";
            }

            // Fix for "cn"
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

        protected IQueryable<TEntity> TrySortBy<TEntity>(IQueryable<TEntity> entities, CollectionQueryParameters parameters)
        {
            foreach (var sortBy in parameters.SortByList)
            {
                if (string.IsNullOrEmpty(sortBy.SortProperty) || string.IsNullOrEmpty(sortBy.SortDirection))
                {
                    continue;
                }

                try
                {
                    var (sortByPropertyExpression, sortByParameterExpression) = this.GetPropertyExpression<TEntity>(sortBy.SortProperty);

                    var newSortByPropertyExpression = Expression.Lambda<Func<TEntity, object>>(sortByPropertyExpression, sortByParameterExpression);


                    entities = !string.Equals(sortBy.SortDirection, QueryParameterConstants.SortDirectionDescending, StringComparison.InvariantCultureIgnoreCase)
                        ? entities != typeof(IOrderedQueryable<TEntity>) ? entities.OrderBy(newSortByPropertyExpression) : (entities as IOrderedQueryable<TEntity>).ThenBy(newSortByPropertyExpression)
                        : entities != typeof(IOrderedQueryable<TEntity>) ? entities.OrderByDescending(newSortByPropertyExpression) : (entities as IOrderedQueryable<TEntity>).ThenByDescending(newSortByPropertyExpression);
                }
                catch (NotImplementedException notImplementedException)
                {
                    throw new NotImplementedException($"Sorting by '{sortBy.SortProperty}' is not supported yet!", notImplementedException);
                }
                catch (PropertyIsNotSimpleTypeException propertyIsNotSimpleTypeException)
                {
                    throw new NotImplementedException($"Sorting by '{sortBy.SortProperty}' is not supported yet!", propertyIsNotSimpleTypeException);
                }
                catch (PropertyNotMappedException propertyNotMappedException)
                {
                    throw new NotImplementedException($"Sorting by '{sortBy.SortProperty}' is not supported yet!", propertyNotMappedException);
                }
                catch (PropertyDoesNotExistException propertyDoesNotExistException)
                {
                    throw new SortException(sortBy.SortProperty, $"There is no property with name '{sortBy.SortProperty}' to sort by, please check the spelling.", propertyDoesNotExistException);
                }
            }

            return entities;
        }

        protected IQueryable<TEntity> TryFilter<TEntity>(IQueryable<TEntity> entities, CollectionQueryParameters parameters)
        {
            foreach (var filter in parameters.FiltersList)
            {
                if (string.IsNullOrEmpty(filter.FilterProperty) || string.IsNullOrEmpty(filter.FilterValue))
                {
                    continue;
                }

                try
                {
                    var (filterPropertyExpression, filterParameterExpression) = this.GetPropertyExpression<TEntity>(filter.FilterProperty);

                    var filterPropertyExpressionToString = Expression.Call(filterPropertyExpression, "ToString", Type.EmptyTypes);

                    var filterPropertyExpressionToLower = Expression.Call(filterPropertyExpressionToString, "ToLower", Type.EmptyTypes);

                    var filterPropertyExpressionContains = Expression.Call(filterPropertyExpressionToLower, "Contains", Type.EmptyTypes, Expression.Constant(filter.FilterValue, typeof(string)));

                    var filterPropertyExpressionIndexOf = Expression.Call(filterPropertyExpressionToLower, "IndexOf", Type.EmptyTypes, Expression.Constant(filter.FilterValue, typeof(string)));

                    var filterPropertyWhere = Expression.Lambda<Func<TEntity, bool>>(filterPropertyExpressionContains, filterParameterExpression);

                    var filterPropertyOrderBy = Expression.Lambda<Func<TEntity, int>>(filterPropertyExpressionIndexOf, filterParameterExpression);

                    var filterPropertyThenBy = Expression.Lambda<Func<TEntity, int>>(Expression.Property(filterPropertyExpressionToLower, "Length"), filterParameterExpression); ;

                    entities = entities.Where(filterPropertyWhere.Compile()).AsQueryable().OrderBy(filterPropertyOrderBy).ThenBy(filterPropertyThenBy);
                }
                catch (NotImplementedException notImplementedException)
                {
                    throw new NotImplementedException($"Filtering by '{filter.FilterProperty}' is not supported yet!", notImplementedException);
                }
                catch (PropertyIsNotSimpleTypeException propertyIsNotSimpleTypeException)
                {
                    throw new NotImplementedException($"Filtering by '{filter.FilterProperty}' is not supported yet!", propertyIsNotSimpleTypeException);
                }
                catch (PropertyNotMappedException propertyNotMappedException)
                {
                    throw new NotImplementedException($"Filtering by '{filter.FilterProperty}' is not supported yet!", propertyNotMappedException);
                }
                catch (PropertyDoesNotExistException propertyDoesNotExistException)
                {
                    throw new FilterException(filter.FilterProperty, $"There is no property with name '{filter.FilterProperty}' to filter by, please check the spelling.", propertyDoesNotExistException);
                }
            }

            return entities;
        }

        private (Expression PropertyExpression, ParameterExpression ParameterExpresiion) GetPropertyExpression<TEntity>(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName), $"The '{nameof(propertyName)}' cannot be null or empty.");
            }

            var typeMaps = this._mapper.ConfigurationProvider.GetAllTypeMaps().AsQueryable();

            var entityTypeMap = this._mapper.ConfigurationProvider.GetAllTypeMaps().AsQueryable().FirstOrDefault(t => t.SourceType == typeof(TEntity));

            if (entityTypeMap == null)
            {
                throw new NotImplementedException();
            }

            var property = entityTypeMap.DestinationType.GetProperties().FirstOrDefault(property => property.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

            if (property == null)
            {
                throw new PropertyDoesNotExistException($"The propery '{propertyName}' was not found.");
            }

            if (!property.PropertyType.IsSimpleType())
            {
                throw new PropertyIsNotSimpleTypeException("Sorting by complex types is not supported yet.");
            }

            var entityPropertyMap = entityTypeMap.PropertyMaps.First(p => p.IsMapped && p.DestinationMember != null && p.DestinationMember.Name == property.Name);

            if (entityPropertyMap == null)
            {
                throw new PropertyNotMappedException($"The property '{propertyName} is not mapped.'");
            }

            var originalExpression = entityPropertyMap.CustomMapExpression;

            var expression = Expression.Convert(originalExpression.Body, typeof(object));
            var parameterExpression = originalExpression.Parameters.First();

            return (expression, parameterExpression);
        }
    }
}
