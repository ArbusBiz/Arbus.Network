using Arbus.Network.Abstractions;
using Arbus.Network.Exceptions;
using Arbus.Network.Extensions;

namespace Arbus.Network.UnitTests.Tests;

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

    [Test]
    public void GetTimeoutCts_InfiniteTimeSpan_AssertNullCts()
    {
        using HttpRequestMessage timeout = new();
        timeout.SetTimeout(Timeout.InfiniteTimeSpan);

        var cts = NativeHttpClient.GetTimeoutCts(timeout, default);

        Assert.IsNull(cts);
    }

    [Test]
    public void GetTimeoutCts_NotInfiniteTimeSpan_AssertNotNUllCts()
    {
        using HttpRequestMessage timeout = new();
        timeout.SetTimeout(TimeSpan.FromSeconds(1));

        using var cts = NativeHttpClient.GetTimeoutCts(timeout, default);
        
        Assert.NotNull(cts);
    }

    [Test]
    public void GetTimeoutCts_CancellFirstToken_AssertSecondIsCancelled()
    {
        using HttpRequestMessage timeout = new();
        timeout.SetTimeout(TimeSpan.FromSeconds(1));
        using var cts1 = new CancellationTokenSource();
        cts1.Cancel();

        using var cts2 = NativeHttpClient.GetTimeoutCts(timeout, cts1.Token);
        
        Assert.IsTrue(cts2?.Token.IsCancellationRequested);
    }
}