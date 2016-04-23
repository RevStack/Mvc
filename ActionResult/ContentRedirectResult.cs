using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RevStack.Mvc
{
    public class ContentRedirectResult<T> : IHttpActionResult where T : class
    {
        private HttpRequestMessage _request;
        private HttpStatusCode _statusCode;
        private string _url;
        private string _header = "X-Location";
        private T _value = null;
        public ContentRedirectResult(HttpRequestMessage request, string url, T value)
        {
            _request = request;
            _statusCode = HttpStatusCode.RedirectMethod;
            _url = url;
            _value = value;
        }
        public ContentRedirectResult(HttpRequestMessage request, HttpStatusCode statusCode, string url, T value)
        {
            _request = request;
            _statusCode = statusCode;
            _url = url;
            _value = value;
        }
        public ContentRedirectResult(HttpRequestMessage request, string url, string header, T value)
        {
            _request = request;
            _statusCode = HttpStatusCode.RedirectMethod;
            _url = url;
            _value = value;
        }
        public ContentRedirectResult(HttpRequestMessage request, HttpStatusCode statusCode, string url, string header, T value)
        {
            _request = request;
            _statusCode = statusCode;
            _url = url;
            _value = value;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var msg = _request.CreateResponse(_statusCode, _value);
            msg.Headers.Add(_header, _url);

            return Task.FromResult(msg);
        }

    }

    public class ContentRedirectResult : IHttpActionResult
    {
        private HttpRequestMessage _request;
        private HttpStatusCode _statusCode;
        private string _url;
        private string _header = "X-Location";
        public ContentRedirectResult(HttpRequestMessage request, string url)
        {
            _request = request;
            _statusCode = HttpStatusCode.RedirectMethod;
            _url = url;
        }
        public ContentRedirectResult(HttpRequestMessage request, HttpStatusCode statusCode, string url)
        {
            _request = request;
            _statusCode = statusCode;
            _url = url;
        }
        public ContentRedirectResult(HttpRequestMessage request, string url, string header)
        {
            _request = request;
            _statusCode = HttpStatusCode.RedirectMethod;
            _url = url;
            _header = header;
        }
        public ContentRedirectResult(HttpRequestMessage request, HttpStatusCode statusCode, string url, string header)
        {
            _request = request;
            _statusCode = statusCode;
            _url = url;
            _header = header;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var msg = _request.CreateResponse(_statusCode);
            msg.Headers.Add(_header, _url);

            return Task.FromResult(msg);
        }

    }
}
