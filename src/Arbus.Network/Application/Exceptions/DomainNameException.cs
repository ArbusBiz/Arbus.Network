namespace Arbus.Network.Application.Exceptions;

public class DomainNameException : NetworkException
{
    public string Host { get; }

    public DomainNameException(string host) : base("Cannot resolve server address or there is no Internet connection.")
    {
        Host = host;
    }
}