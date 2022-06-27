namespace Arbus.Network.Abstractions;

public interface IHttpContext
{
    Task RunEndpoint(ApiEndpoint endpoint);
    Task<T> RunEndpoint<T>(ApiEndpoint<T> endpoint);
    Task<TStream> RunStreamEndpoint<TStream>(ApiEndpoint<TStream> endpoint) where TStream : Stream;
    Task<THttpContent> RunHttpContentEndpoint<THttpContent>(ApiEndpoint<THttpContent> endpoint) where THttpContent : HttpContent;
}