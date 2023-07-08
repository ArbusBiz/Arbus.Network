namespace Arbus.Network;

public interface INetworkMonitor
{
    bool IsNetworkAvailable { get; }
    event EventHandler<bool>? NetworkAvailabilityChanged;
}

[Obsolete($"Use {nameof(INetworkMonitor)}")]
public interface INetworkManager : INetworkMonitor
{
    
}