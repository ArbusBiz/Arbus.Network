using Arbus.Network.Abstractions;
using Arbus.Network.Implementations;

namespace Arbus.Network.UnitTests.Tests;

public class HttpClientContextTests
{
    [Test]
    public async Task RunEndpointInternal_InvokesSendRequestWithCancellationTokenFromEndpoint()
    {
        using CancellationTokenSource cts = new();
        CancellationToken cancellationToken = cts.Token;

        var mockApiEndpoint = Mock.Of<ApiEndpoint>(x => x.CancellationToken == cancellationToken && x.Path == "http://localhost" && x.Method == HttpMethod.Get);
        var mockDefaultHttpClient = new Mock<INativeHttpClient>();
        mockDefaultHttpClient.Setup(x => x.SendRequest(It.IsAny<HttpRequestMessage>(), cancellationToken, It.IsAny<HttpCompletionOption>())).ReturnsAsync(new HttpResponseMessage());

        HttpClientContext httpClientContext = new(mockDefaultHttpClient.Object);

        await httpClientContext.RunEndpointInternal(mockApiEndpoint);
    }

    [Test]
    public async Task RunEndpointInternal_InvokesSendRequestWithDefaultCancellationToken()
    {
        var mockApiEndpoint = Mock.Of<ApiEndpoint>(x => x.Path == "http://localhost" && x.Method == HttpMethod.Get);
        var mockDefaultHttpClient = new Mock<INativeHttpClient>();
        mockDefaultHttpClient.Setup(x => x.SendRequest(It.IsAny<HttpRequestMessage>(), default, It.IsAny<HttpCompletionOption>())).ReturnsAsync(new HttpResponseMessage());

        HttpClientContext httpClientContext = new(mockDefaultHttpClient.Object);

        await httpClientContext.RunEndpointInternal(mockApiEndpoint);
    }
}