namespace Arbus.Network.Exceptions;

public class NoNetoworkConnectionException : NetworkException
{
    public NoNetoworkConnectionException() : base("No network connection.")
    {
    }
}