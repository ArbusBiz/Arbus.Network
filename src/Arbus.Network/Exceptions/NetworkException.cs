using System.Net;

namespace Arbus.Network.Exceptions;

public class NetworkException : Exception
{
    public HttpStatusCode? StatusCode { get; }

    public ProblemDetails? ProblemDetails { get; }
    
    public ApiEndpoint? Endpoint { get; }

    public NetworkException(HttpStatusCode httpStatusCode, ProblemDetails problemDetails, ApiEndpoint endpoint)
        : this(httpStatusCode, problemDetails.Detail ?? problemDetails.Title ?? string.Empty, endpoint)
    {
        ProblemDetails = problemDetails;
    }

    public NetworkException(HttpStatusCode httpStatusCode, string message, ApiEndpoint endpoint) : this(message, endpoint)
    {
        StatusCode = httpStatusCode;
    }

    public NetworkException(string message, ApiEndpoint endpoint) : base(message)
    {
        Endpoint = endpoint;
    }

    public NetworkException(string message) : base(message)
    {
    }
}