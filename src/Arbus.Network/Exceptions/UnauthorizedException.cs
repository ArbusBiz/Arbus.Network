using Arbus.Network.Abstractions;
using System.Net;

namespace Arbus.Network.Exceptions;

public class UnauthorizedException : NetworkException
{
    public UnauthorizedException(string content, ApiEndpoint endpoint) 
        : base(HttpStatusCode.Unauthorized, content, endpoint)
    {
    }

    public UnauthorizedException(ProblemDetails problemDetails, ApiEndpoint endpoint) 
        : base(HttpStatusCode.Unauthorized, problemDetails, endpoint)
    {
    }
}