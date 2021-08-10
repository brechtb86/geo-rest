namespace Geo.Rest.Domain.Models.Errors
{
    /// <summary>
    /// The validation error response model class.
    /// </summary>
    public class ValidationError : Error
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError" /> class.
        /// </summary>
        /// <param name="message">The friendly message.</param>
        /// <param name="requestUri">The request URI.</param>
        public ValidationError(string message, string requestUri) : base(message, requestUri)
        {

        }
    }
}
