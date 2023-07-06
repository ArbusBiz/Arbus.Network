namespace Arbus.Network;

public interface INetworkManager
{
    bool IsNetworkAvailable { get; }
    event EventHandler<bool>? NetworkAvailabilityChanged;
}