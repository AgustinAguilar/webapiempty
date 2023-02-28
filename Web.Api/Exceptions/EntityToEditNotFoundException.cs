using System;
using System.Net;

namespace Web.Api.Exceptions
{
    public class EntityToEditNotFoundException : WebApiException
    {
        const string _message = "No se encontró la entidad";

        public EntityToEditNotFoundException() : base(HttpStatusCode.BadRequest, _message)
        {

        }

        public EntityToEditNotFoundException(HttpStatusCode statusCode) : base(statusCode, _message)
        {
        }

        public EntityToEditNotFoundException(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
        }

        public EntityToEditNotFoundException(HttpStatusCode statusCode, Exception innerException) : base(statusCode, innerException)
        {
        }
    }
}
