using Geo.Rest.Domain.Exceptions;
using Geo.Rest.Domain.Models.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Geo.Rest.ExceptionFilters
{
    /// <summary>
    /// Returns friendly responses when exceptions occur in the application.
    /// </summary>
    public class ReturnFriendlyExceptionsAttribute : ExceptionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            
            var request = context.HttpContext.Request;

            var requestUri = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";

            var exception = context.Exception;

            switch (exception)
            {
                #region Business exception handling

                case NotFoundException resourceNotFoundException: // returns a HTTP status code 404 with a body
                    {
                        var resourceNotFoundError = new Error($"This endpoint is correct but the requested resource of type '{resourceNotFoundException.ResourceType.Name}' with identifier '{resourceNotFoundException.Id}' could not be found.", requestUri);

                        context.Result = new NotFoundObjectResult(resourceNotFoundError);

                        //logger?.Warning(exception);

                        break;
                    }

                case SortException sortException: // returns a HTTP status code 400 with a body
                    {
                        var argumentError = new Error($"There is something wrong with your request.", requestUri, sortException.Message);

                        context.Result = new BadRequestObjectResult(argumentError);

                        //logger?.Warning(exception);

                        break;
                    }

                #endregion

                #region Database excption handling

                // We need to prevent details to be exposed to the user (database names or user names, for example when a authentication problem occurs.
                // The exception message is then something like this: Cannot open database \"MDM_POC_Energy_BE\" requested by the login. The login failed.\r\nLogin failed for user 'CONSEUR\\CO-CORMG-AGWAP1$'.
                // So we don't set the detail message.

                case SqlException sqlException:
                    {
                        // For testing purposes all the details are included.
                        var sqlError = new Error($"Something bad happened when trying to access the database. {sqlException.Server}", requestUri, exception.Message);

                        context.Result = new ObjectResult(sqlError) { StatusCode = (int)HttpStatusCode.InternalServerError };

                        //logger?.Error(exception);

                        break;
                    }

                #endregion

                #region Default exeption handling

                case NotImplementedException notImplementedException: // returns a HTTP status code 501 with a body
                    {
                        var notImplementedError = new Error($"This endpoint is correct but some logic has not been implemented yet.", requestUri, notImplementedException.Message);

                        context.Result = new ObjectResult(notImplementedError) { StatusCode = (int)HttpStatusCode.NotImplemented };

                        //logger?.Warning(exception);

                        break;
                    }
                default:  // returns a HTTP status code 500 with a body
                    {
                        var generalError = new Error("Something bad happened when processing your request.", requestUri, exception.Message);

                        context.Result = new ObjectResult(generalError) { StatusCode = (int)HttpStatusCode.InternalServerError };

                        //logger?.Error(context.Exception);

                        break;
                    }

                    #endregion
            }

            base.OnException(context);
        }
    }
}
