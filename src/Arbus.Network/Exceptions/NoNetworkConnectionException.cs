namespace Arbus.Network.Exceptions;

public class NoNetworkConnectionException : NetworkException
{
    public NoNetworkConnectionException() : base("No network connection.")
    {
    }
}