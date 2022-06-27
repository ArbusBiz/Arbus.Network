namespace Arbus.Network.Exceptions;

public class HttpTimeoutException : NetworkException
{
    public HttpTimeoutException() : base("The connection has timed out")
    {
    }
}