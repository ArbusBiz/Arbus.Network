using System.Net;

namespace Arbus.Network.Application.Exceptions;

public class UnauthorizedException : NetworkException
{
    public UnauthorizedException(string content) : base(HttpStatusCode.Unauthorized, content)
    {
    }

    public UnauthorizedException(ProblemDetails problemDetails) : base(HttpStatusCode.Unauthorized, problemDetails)
    {
    }
}