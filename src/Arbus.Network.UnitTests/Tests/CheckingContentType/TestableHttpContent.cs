using System.Net;
using System.Net.Http.Headers;

namespace Arbus.Network.UnitTests.Tests.CheckingContentType;

public class TestableHttpContent : HttpContent
{
    public TestableHttpContent(string contentType)
    {
        Headers.ContentType = new MediaTypeHeaderValue(contentType);
    }
    
    protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) => throw new NotImplementedException();

    protected override bool TryComputeLength(out long length) => throw new NotImplementedException();
}