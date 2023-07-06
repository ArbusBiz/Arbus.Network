using System.Net;

namespace Arbus.Network.Exceptions;

public class NetworkException : Exception
{
    public HttpStatusCode? StatusCode { get; }

    public ProblemDetails? ProblemDetails { get; }

    public NetworkException(HttpStatusCode httpStatusCode, ProblemDetails problemDetails)
        : this(httpStatusCode, problemDetails.Detail ?? problemDetails.Title ?? string.Empty)
    {
        ProblemDetails = problemDetails;
    }

    public NetworkException(HttpStatusCode httpStatusCode, string message) : this(message)
    {
        StatusCode = httpStatusCode;
    }

    public NetworkException(string message) : base(message)
    {
    }
}