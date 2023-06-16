using Arbus.Network.Abstractions;
using System.Net;

namespace Arbus.Network.Exceptions;

public class BadRequestException : NetworkException
{
    public BadRequestException(string stringContent, ApiEndpoint endpoint) 
        : base(HttpStatusCode.BadRequest, stringContent, endpoint)
    {
    }

    public BadRequestException(ProblemDetails problemDetails, ApiEndpoint endpoint) 
        : base(HttpStatusCode.BadRequest, problemDetails, endpoint)
    {
    }
}
