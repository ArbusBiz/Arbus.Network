namespace Arbus.Network;

public interface INetworkMonitor
{
    bool IsNetworkAvailable { get; }
    event EventHandler<bool>? NetworkAvailabilityChanged;
}