using Arbus.Network.ContentSerializers;

namespace Arbus.Network.Abstractions;

public abstract class ApiEndpoint
{
    private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(10);

    public abstract string Path { get; }

    public abstract HttpMethod Method { get; }

    public virtual TimeSpan Timeout => _defaultTimeout;

    public virtual Dictionary<string, string>? AdditionalHeaders { get; }

    public CancellationToken? CancellationToken { get; set; }

    public virtual HttpContent? CreateContent() => default;
}

public abstract class ApiEndpoint<TResponseContent> : ApiEndpoint
{
    public virtual Task<TResponseContent> GetResponse(HttpContent httpContent) => JsonContentSerializer.Deserialize<TResponseContent>(httpContent);
}