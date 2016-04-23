using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RevStack.Mvc
{
    public class ContentErrorResult : IHttpActionResult
    {
        private HttpRequestMessage _request;
        private HttpStatusCode _statusCode;
        private string _errorMessage;
        public ContentErrorResult(HttpRequestMessage request, string errorMessage)
        {
            _request = request;
            _statusCode = HttpStatusCode.BadRequest;
            _errorMessage = errorMessage;
        }
        public ContentErrorResult(HttpRequestMessage request, HttpStatusCode statusCode, string errorMessage)
        {
            _request = request;
            _statusCode = statusCode;
            _errorMessage = errorMessage;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var msg = _request.CreateErrorResponse(_statusCode, new HttpError(_errorMessage));
            return Task.FromResult(msg);
        }
    }
}
