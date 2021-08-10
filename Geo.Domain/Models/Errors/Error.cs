namespace Geo.Rest.Domain.Models.Errors
{
    /// <summary>
    /// The error response model class.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// The friendly message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The detailed message.
        /// </summary>
        public string DetailMessage { get; }

        /// <summary>
        /// The request URI.
        /// </summary>
        public string RequestUri { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error" /> class.
        /// </summary>
        /// <param name="message">The friendly message.</param>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="detailMessage">A more detailed message.</param>
        public Error(string message, string requestUri, string detailMessage = null)
        {
            this.Message = message;
            this.RequestUri = requestUri;
            this.DetailMessage = detailMessage;
        }
    }
}
