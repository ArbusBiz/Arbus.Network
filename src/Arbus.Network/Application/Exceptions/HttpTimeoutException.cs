namespace Arbus.Network.Application.Exceptions;

public class HttpTimeoutException : NetworkException
{
    public HttpTimeoutException() : base("The connection has timed out")
    {
    }
}