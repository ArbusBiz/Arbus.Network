using System.Net;

namespace Arbus.Network.Exceptions;

public static class NetworkExceptionFactory
{
    public static NetworkException Create(HttpStatusCode statusCode, string content) => statusCode switch
    {
        HttpStatusCode.Unauthorized => new UnauthorizedException(content),
        HttpStatusCode.BadRequest => new BadRequestException(content),
        _ => new NetworkException(statusCode, content)
    };

    public static NetworkException Create(HttpStatusCode statusCode, ProblemDetails problemDetails) => statusCode switch
    {
        HttpStatusCode.Unauthorized => new UnauthorizedException(problemDetails),
        HttpStatusCode.BadRequest => new BadRequestException(problemDetails),
        _ => new NetworkException(statusCode, problemDetails)
    };
}