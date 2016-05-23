using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RevStack.Mvc
{
    /// <summary>
    /// culled largely from : http://weblog.west-wind.com/posts/2013/Apr/15/WebAPI-Getting-Headers-QueryString-and-Cookie-Values
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Returns a dictionary of QueryStrings that's easier to work with 
        /// than GetQueryNameValuePairs KevValuePairs collection.
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQueryStrings(this HttpRequestMessage request)
        {
            return request.GetQueryNameValuePairs()
                          .ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns an individual querystring value
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetQueryString(this HttpRequestMessage request, string key)
        {
            // IEnumerable<KeyValuePair<string,string>> - right!
            var queryStrings = request.GetQueryNameValuePairs();
            if (queryStrings == null)
                return null;

            var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, true) == 0);
            if (string.IsNullOrEmpty(match.Value))
                return null;

            return match.Value;
        }

        /// <summary>
        /// Returns an individual HTTP Header value
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetHeader(this HttpRequestMessage request, string key)
        {
            IEnumerable<string> keys = null;
            if (!request.Headers.TryGetValues(key, out keys))
                return null;

            return keys.First();
        }

        /// <summary>
        /// Retrieves an individual cookie from the cookies collection
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookie(this HttpRequestMessage request, string cookieName)
        {
            CookieHeaderValue cookie = request.Headers.GetCookies(cookieName).FirstOrDefault();
            if (cookie != null)
                return cookie[cookieName].Value;

            return null;
        }

        public static string ToQueryString(this HttpRequestMessage request)
        {
            var dict = request.GetQueryStrings();
            if (dict.Count() == 0) return "";
            string q = "";
            foreach(var d in dict)
            {
                q += d.Key + "=" + HttpUtility.UrlEncode(d.Value) + "&";
            }

            return q.TrimLastChar();
        }

        public static string ToQueryString(this HttpRequestMessage request, IEnumerable<string> excludeKeys)
        {
            var dict = request.GetQueryStrings();
            if (dict.Count() == 0) return "";
            string q = "";
            foreach (var d in dict)
            {
                if(excludeKeys.Where(x=>x==d.Key).Count()==0)
                {
                    q += d.Key + "=" + HttpUtility.UrlEncode(d.Value) + "&";
                }
                
            }

            return q.TrimLastChar();
        }

        public static string ToQueryString(this HttpRequestMessage request,bool includeSeparator)
        {
            var q = request.ToQueryString();
            if(includeSeparator)
            {
                return "$" + q;
            }
            else
            {
                return q;
            }
        }

        public static string ToQueryString(this HttpRequestMessage request, IEnumerable<string> excludeKeys, bool includeSeparator)
        {
            var q = request.ToQueryString(excludeKeys);
            if (q.Length == 0) return q;
            if (includeSeparator)
            {
                return "$" + q;
            }
            else
            {
                return q;
            }
        }

    }
}
