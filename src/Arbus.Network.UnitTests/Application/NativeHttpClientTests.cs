using Arbus.Network.Abstractions;
using Arbus.Network.Exceptions;

namespace Arbus.Network.UnitTests.Application;

public class NativeHttpClientTests : TestFixture
{
    [Test]
    public void EnsureNoTimeout_CancellationRequested_ThrowsHttpTimeoutException()
    {
        var canceallationTokenSource = new CancellationTokenSource();
        canceallationTokenSource.Cancel();

        Assert.Throws<HttpTimeoutException>(() => NativeHttpClient.EnsureNoTimeout(canceallationTokenSource));
    }

    [Test]
    public void EnsureNoTimeout_CancellationNotRequested_NoException()
    {
        var canceallationTokenSource = new CancellationTokenSource();

        Assert.DoesNotThrow(() => NativeHttpClient.EnsureNoTimeout(canceallationTokenSource));
    }

    [Test]
    public void EnsureNetworkAvailable_NetworkNotAvailable_ThrowsNoNetworkConnectionAvailableException()
    {
        var mockNetworkManager = CreateMock<INetworkManager>();
        mockNetworkManager.SetupGet(x => x.IsNetworkAvailable).Returns(false);

        NativeHttpClient nativeHttpClient = new(mockNetworkManager.Object);

        Assert.Throws<NoNetoworkConnectionException>(() => nativeHttpClient.EnsureNetworkAvailable());
    }

    [Test]
    public void EnsureNetworkAvailable_NetworkAvailable_NoException()
    {
        var mockNetworkManager = CreateMock<INetworkManager>();
        mockNetworkManager.SetupGet(x => x.IsNetworkAvailable).Returns(true);

        NativeHttpClient nativeHttpClient = new(mockNetworkManager.Object);

        Assert.DoesNotThrow(() => nativeHttpClient.EnsureNetworkAvailable());
    }
}
