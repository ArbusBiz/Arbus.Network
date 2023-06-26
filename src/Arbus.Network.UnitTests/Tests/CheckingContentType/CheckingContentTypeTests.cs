using Arbus.Network.Extensions;
using FluentAssertions;

namespace Arbus.Network.UnitTests.Tests.CheckingContentType;

public class CheckingContentTypeTests
{
    [Test]
    public void HasApplicationJsonContentType_ContentTypeIsApplicationJson_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Application.Json);

        var result = content.HasApplicationJsonContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasApplicationTextContentType_ContentTypeIsApplicationText_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Application.Text);

        bool result = content.HasApplicationTextContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasApplicationProtobufContentType_ContentTypeIsApplicationProtobuf_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Application.Protobuf);

        bool result = content.HasApplicationProtobufContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasApplicationProblemJsonContentType_ContentTypeIsApplicationProblemJson_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Application.ProblemJson);

        bool result = content.HasApplicationProblemJsonContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasTextPlainContentType_ContentTypeIsTextPlain_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Text.Plain);

        bool result = content.HasTextPlainContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasMultipartFormDataContentType_ContentTypeIsMultipartFormData_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Multipart.FormData);

        bool result = content.HasMultipartFormDataContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasMultipartMixedContentType_ContentTypeIsMultipartMixed_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Multipart.Mixed);

        bool result = content.HasMultipartMixedContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasMultipartAlternativeContentType_ContentTypeIsMultipartAlternative_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Multipart.Alternative);

        bool result = content.HasMultipartAlternativeContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasMultipartRelatedContentType_ContentTypeIsMultipartRelated_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Multipart.Related);

        bool result = content.HasMultipartRelatedContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasImageSvgXmlContentType_ContentTypeIsImageSvgXml_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.SvgXml);

        bool result = content.HasImageSvgXmlContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasImageGifContentType_ContentTypeIsImageGif_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.Gif);

        bool result = content.HasImageGifContentType();

        result.Should().BeTrue();
    }

    [Test]
    public void HasImageJpegContentType_ContentTypeIsImageJpeg_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.Jpeg);

        bool result = content.HasImageJpegContentType();

        result.Should().BeTrue();
    }
    
    [Test]
    public void HasImagePngContentTypeContentTypeIsImagePngReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.Png);

        bool result = content.HasImagePngContentType();

        result.Should().BeTrue();
    }
    
    [Test]
    public void HasImageTiffContentType_ContentTypeIsImageTiff_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.Tiff);

        bool result = content.HasImageTiffContentType();

        result.Should().BeTrue();
    }
    
    [Test]
    public void HasImageVndMicrosoftIconContentType_ContentTypeIsImageVndMicrosoftIcon_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.VndMicrosoftIcon);

        bool result = content.HasImageVndMicrosoftIconContentType();

        result.Should().BeTrue();
    }
    
    [Test]
    public void HasImageXIconContentType_ContentTypeIsImageXIcon_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.XIcon);

        bool result = content.HasImageXIconContentType();

        result.Should().BeTrue();
    }
    
    [Test]
    public void HasImageVndDjvuContentType_ContentTypeIsImageVndDjvu_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.VndDjvu);

        bool result = content.HasImageVndDjvuContentType();

        result.Should().BeTrue();
    }
}