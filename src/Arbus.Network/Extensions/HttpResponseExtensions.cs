namespace Arbus.Network.Extensions;

public static class HttpResponseExtensions
{
    public static string ToReadableString(this HttpResponseMessage response)
        => $"\n{response.RequestMessage.Method} {response.RequestMessage.RequestUri} {response.StatusCode} {(int)response.StatusCode}";
}
