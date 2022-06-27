using System.Net;

namespace Arbus.Network.Exceptions;

public class BadRequestException : NetworkException
{
    public BadRequestException(string stringContent) : base(HttpStatusCode.BadRequest, stringContent)
    {
    }

    public BadRequestException(ProblemDetails problemDetails) : base(HttpStatusCode.BadRequest, problemDetails)
    {
    }
}
