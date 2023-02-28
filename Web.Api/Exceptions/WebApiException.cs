using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Web.Api.Exceptions
{
    public class WebApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public WebApiException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public WebApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public WebApiException(HttpStatusCode statusCode, Exception inner) : this(statusCode, inner.ToString()) { }

        public WebApiException(HttpStatusCode statusCode, JObject errorObject) : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }

        public WebApiException(string message) : base(message)
        {
        }
    }
}
