using System;
using System.Web;

namespace RevStack.Mvc
{
    public class UriUtility
    {
        private HttpRequestBase _request;

        public UriUtility(HttpRequestBase request)
        {
            _request = request;
        }

        public string Host
        {
            get
            {
                Uri uri = new Uri(_request.Url.ToString());
                string result = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
                if (uri.Port == 80) return result;
                else return result += ":" + uri.Port;
            }
        }

        public string Path
        {
            get
            {
                return _request.Url.AbsolutePath;
            }
        }

        public int Port
        {
            get
            {
                return _request.Url.Port;
            }
        }

        public string Query
        {
            get
            {
                return _request.Url.Query;
            }
        }

        public string Domain
        {
            get
            {
                return _request.Url.Host;
            }
        }

        public string Protocol
        {
            get
            {
                return _request.Url.Scheme;
            }
        }

        public string PathAndQuery
        {
            get
            {
                return _request.Url.PathAndQuery;
            }
        }

        public string Url
        {
            get
            {
                return _request.Url.OriginalString;
            }
        }
    }
}
