namespace Arbus.Network.Extensions;

public static class HttpRequestExtensions
{
    private const string _key = "Timeout";
    private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(100);
#if NET6_0_OR_GREATER
    private static HttpRequestOptionsKey<TimeSpan> _optionsKey = new(_key);
#endif

    public static void SetTimeout(this HttpRequestMessage request, TimeSpan timeout)
#if NETSTANDARD
        => request.Properties[_key] = timeout;
#else
        => request.Options.Set(_optionsKey, timeout);
#endif

    public static TimeSpan GetTimeout(this HttpRequestMessage request)
    {
#if NETSTANDARD
        if (request.Properties.TryGetValue(_key, out object value))
            return (TimeSpan)value;
        return _defaultTimeout;
#else
        return request.Options.TryGetValue(_optionsKey, out var value)
            ? value : _defaultTimeout;
#endif
    }
}