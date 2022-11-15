namespace Arbus.Network.Extensions;

public static class HttpResponseExtensions
{
    [Obsolete("Do not use. Will be removed.")]
    public static string ToReadableString(this HttpResponseMessage response)
        => $"\n{response.RequestMessage.Method} {response.RequestMessage.RequestUri} {response.StatusCode} {(int)response.StatusCode}";
}
