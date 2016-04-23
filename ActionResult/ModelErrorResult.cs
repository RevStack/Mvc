using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace RevStack.Mvc
{
    public class ModelErrorResult : IHttpActionResult
    {
        private HttpRequestMessage _request;
        private HttpStatusCode _statusCode;
        private IEnumerable<ModelError> _errors;
        public ModelErrorResult(HttpRequestMessage request, IEnumerable<ModelError> errors)
        {
            _request = request;
            _errors = errors;
            _statusCode = HttpStatusCode.BadRequest;
        }
        public ModelErrorResult(HttpRequestMessage request, IEnumerable<ModelError> errors, HttpStatusCode statusCode)
        {
            _request = request;
            _errors = errors;
            _statusCode = statusCode;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var errorMessage = _errors.Select(x => x.ErrorMessage).FirstOrDefault();
            var msg = _request.CreateErrorResponse(_statusCode, new HttpError(errorMessage));
            return Task.FromResult(msg);
        }
    }
}
