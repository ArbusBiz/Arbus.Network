using System.Net;

namespace Arbus.Network.Application.Exceptions;

public class NetworkException : Exception
{
    public HttpStatusCode? StatusCode { get; }

    public ProblemDetails? ProblemDetails { get; }

    public NetworkException(HttpStatusCode httpStatusCode, string stringContent) : base(stringContent)
    {
        StatusCode = httpStatusCode;
    }

    public NetworkException(HttpStatusCode httpStatusCode, ProblemDetails problemDetails)
        : this(httpStatusCode, problemDetails.Detail ?? problemDetails.Title ?? string.Empty)
    {
        ProblemDetails = problemDetails;
    }

    public NetworkException(string message) : base(message)
    {
    }
}