namespace Arbus.Network.Abstractions;

public interface INetworkManager
{
    bool IsNetworkAvailable { get; }
    public event EventHandler<bool>? NetworkAvailabilityChanged;
}