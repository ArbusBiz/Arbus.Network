namespace Arbus.Network.Application.Exceptions;

public class NoNetoworkConnectionException : NetworkException
{
    public NoNetoworkConnectionException() : base("No network connection.")
    {
    }
}