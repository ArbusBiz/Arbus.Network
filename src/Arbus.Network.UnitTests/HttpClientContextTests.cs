using Arbus.Network.Abstractions;
using Moq;

namespace Arbus.Network.UnitTests;

public class HttpClientContextTests : TestFixture
{
    [Test]
    public async Task RunEndpointInternal_InvokesSendRequestWithCancellationTokenFromEndpoint()
    {
        using CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        var mockApiEndpoint = Mock.Of<ApiEndpoint>(x => x.CancellationToken == cancellationToken && x.Path == "http://localhost" && x.Method == HttpMethod.Get);
        var mockDefaultHttpClient = CreateMock<IDefaultHttpClient>();
        mockDefaultHttpClient.Setup(x => x.SendRequest(It.IsAny<HttpRequestMessage>(), cancellationToken)).ReturnsAsync(new HttpResponseMessage());

        HttpClientContext httpClientContext = new(mockDefaultHttpClient.Object);

        await httpClientContext.RunEndpointInternal(mockApiEndpoint);
    }

    [Test]
    public async Task RunEndpointInternal_InvokesSendRequestWithDefaultCancellationToken()
    {
        var mockApiEndpoint = Mock.Of<ApiEndpoint>(x => x.Path == "http://localhost" && x.Method == HttpMethod.Get);
        var mockDefaultHttpClient = CreateMock<IDefaultHttpClient>();
        mockDefaultHttpClient.Setup(x => x.SendRequest(It.IsAny<HttpRequestMessage>(), default)).ReturnsAsync(new HttpResponseMessage());

        HttpClientContext httpClientContext = new(mockDefaultHttpClient.Object);

        await httpClientContext.RunEndpointInternal(mockApiEndpoint);
    }
}