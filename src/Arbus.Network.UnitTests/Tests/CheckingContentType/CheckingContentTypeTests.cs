using Arbus.Network.Extensions;
using FluentAssertions;

namespace Arbus.Network.UnitTests.Tests.CheckingContentType;

public class CheckingContentTypeTests
{
    [Test]
    public void IsApplicationJson_ContentTypeIsApplicationJson_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Application.Json);

        var result = content.IsApplicationJson();

        result.Should().BeTrue();
    }

    [Test]
    public void IsApplicationText_ContentTypeIsApplicationText_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Application.Text);

        bool result = content.IsApplicationText();

        result.Should().BeTrue();
    }

    [Test]
    public void IsApplicationProtobuf_ContentTypeIsApplicationProtobuf_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Application.Protobuf);

        bool result = content.IsApplicationProtobuf();

        result.Should().BeTrue();
    }

    [Test]
    public void IsApplicationProblemJson_ContentTypeIsApplicationProblemJson_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Application.ProblemJson);

        bool result = content.IsApplicationProblemJson();

        result.Should().BeTrue();
    }

    [Test]
    public void IsTextPlain_ContentTypeIsTextPlain_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Text.Plain);

        bool result = content.IsTextPlain();

        result.Should().BeTrue();
    }

    [Test]
    public void IsMultipartFormData_ContentTypeIsMultipartFormData_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Multipart.FormData);

        bool result = content.IsMultipartFormData();

        result.Should().BeTrue();
    }

    [Test]
    public void IsMultipartMixed_ContentTypeIsMultipartMixed_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Multipart.Mixed);

        bool result = content.IsMultipartMixed();

        result.Should().BeTrue();
    }

    [Test]
    public void IsMultipartAlternative_ContentTypeIsMultipartAlternative_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Multipart.Alternative);

        bool result = content.IsMultipartAlternative();

        result.Should().BeTrue();
    }

    [Test]
    public void IsMultipartRelated_ContentTypeIsMultipartRelated_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Multipart.Related);

        bool result = content.IsMultipartRelated();

        result.Should().BeTrue();
    }

    [Test]
    public void IsImageSvgXml_ContentTypeIsImageSvgXml_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.SvgXml);

        bool result = content.IsImageSvgXml();

        result.Should().BeTrue();
    }

    [Test]
    public void IsImageGif_ContentTypeIsImageGif_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.Gif);

        bool result = content.IsImageGif();

        result.Should().BeTrue();
    }

    [Test]
    public void IsImageJpeg_ContentTypeIsImageJpeg_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.Jpeg);

        bool result = content.IsImageJpeg();

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsImagePngIsImagePngReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.Png);

        bool result = content.IsImagePng();

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsImageTiff_ContentTypeIsImageTiff_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.Tiff);

        bool result = content.IsImageTiff();

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsImageVndMicrosoftIcon_ContentTypeIsImageVndMicrosoftIcon_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.VndMicrosoftIcon);

        bool result = content.IsImageVndMicrosoftIcon();

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsImageXIcon_ContentTypeIsImageXIcon_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.XIcon);

        bool result = content.IsImageXIcon();

        result.Should().BeTrue();
    }
    
    [Test]
    public void IsImageVndDjvu_ContentTypeIsImageVndDjvu_ReturnsTrue()
    {
        var content = new TestableHttpContent(HttpContentType.Image.VndDjvu);

        bool result = content.IsImageVndDjvu();

        result.Should().BeTrue();
    }
}