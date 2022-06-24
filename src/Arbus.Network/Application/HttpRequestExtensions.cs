namespace Arbus.Network.Application;

public static class HttpRequestExtensions
{
    private const string _timeoutProperyKey = "Timeout";
    private const int _defaultTimeoutInSeconds = 20;

    public static void SetTimeout(this HttpRequestMessage request, TimeSpan timeout)
        => request.Properties[_timeoutProperyKey] = timeout;

    public static TimeSpan GetTimeout(this HttpRequestMessage request)
    {
        return request.Properties.TryGetValue(_timeoutProperyKey, out object value)
            ? (TimeSpan)value
            : TimeSpan.FromSeconds(_defaultTimeoutInSeconds);
    }
}
