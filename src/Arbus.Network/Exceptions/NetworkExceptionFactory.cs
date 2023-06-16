using Arbus.Network.Abstractions;
using System.Net;

namespace Arbus.Network.Exceptions;

public static class NetworkExceptionFactory
{
    public static NetworkException Create(HttpStatusCode statusCode, ProblemDetails problemDetails, ApiEndpoint endpoint) => statusCode switch
    {
        HttpStatusCode.Unauthorized => new UnauthorizedException(problemDetails, endpoint),
        HttpStatusCode.BadRequest => new BadRequestException(problemDetails, endpoint),
        _ => new NetworkException(statusCode, problemDetails, endpoint)
    };

    public static NetworkException Create(HttpStatusCode statusCode, string content, ApiEndpoint endpoint) => statusCode switch
    {
        HttpStatusCode.Unauthorized => new UnauthorizedException(content, endpoint),
        HttpStatusCode.BadRequest => new BadRequestException(content, endpoint),
        _ => new NetworkException(statusCode, content, endpoint)
    };
}