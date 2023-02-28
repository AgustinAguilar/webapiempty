using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Web.Api.Exceptions
{
    public class AuthorizationException : WebApiException
    {
        public AuthorizationException(string message) : base(HttpStatusCode.Unauthorized, message)
        {
        }
    }
}
