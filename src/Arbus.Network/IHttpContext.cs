namespace Arbus.Network;

public interface IHttpClientContext
{
    Task RunEndpoint(ApiEndpoint endpoint);
    Task<TResponse> RunEndpoint<TResponse>(ApiEndpoint<TResponse> endpoint);
}