using Arbus.Network.Exceptions;
using Arbus.Network.Extensions;

namespace Arbus.Network.UnitTests.Tests;

public class NativeHttpClientTests
{
    [Test]
    public void EnsureNoTimeout_CancellationRequested_ThrowsHttpTimeoutException()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        Assert.Throws<HttpTimeoutException>(() => NativeHttpClient.EnsureNoTimeout(cancellationTokenSource));
    }

    [Test]
    public void EnsureNoTimeout_CancellationNotRequested_NoException()
    {
        var cancellationTokenSource = new CancellationTokenSource();

        Assert.DoesNotThrow(() => NativeHttpClient.EnsureNoTimeout(cancellationTokenSource));
    }

    [Test]
    public void EnsureNetworkAvailable_NetworkNotAvailable_ThrowsNoNetworkConnectionAvailableException()
    {
        var mockNetworkManager = new Mock<INetworkManager>();
        mockNetworkManager.SetupGet(x => x.IsNetworkAvailable).Returns(false);

        NativeHttpClient nativeHttpClient = new(mockNetworkManager.Object);

        Assert.Throws<NoNetworkConnectionException>(() => nativeHttpClient.EnsureNetworkAvailable());
    }

    [Test]
    public void EnsureNetworkAvailable_NetworkAvailable_NoException()
    {
        var mockNetworkManager = new Mock<INetworkManager>();
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

        Assert.That(cts, Is.Null);
    }

    [Test]
    public void GetTimeoutCts_NotInfiniteTimeSpan_AssertNotNUllCts()
    {
        using HttpRequestMessage timeout = new();
        timeout.SetTimeout(TimeSpan.FromSeconds(1));

        using var cts = NativeHttpClient.GetTimeoutCts(timeout, default);
        
        Assert.That(cts, Is.Not.Null);
    }

    [Test]
    public void GetTimeoutCts_CancelFirstToken_AssertSecondIsCanceled()
    {
        using HttpRequestMessage timeout = new();
        timeout.SetTimeout(TimeSpan.FromSeconds(1));
        using var cts1 = new CancellationTokenSource();
        cts1.Cancel();

        using var cts2 = NativeHttpClient.GetTimeoutCts(timeout, cts1.Token);
        
        Assert.That(cts2?.Token.IsCancellationRequested, Is.True);
    }
}