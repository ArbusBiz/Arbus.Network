namespace Arbus.Network.Extensions;

public static class HttpContentExtensions
{
    public static bool HasApplicationJsonContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Application.Json);

    public static bool HasApplicationTextContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Application.Text);

    public static bool HasApplicationProtobufContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Application.Protobuf);

    public static bool HasApplicationProblemJsonContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Application.ProblemJson);

    public static bool HasTextPlainContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Text.Plain);

    public static bool HasMultipartFormDataContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Multipart.FormData);

    public static bool HasMultipartMixedContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Multipart.Mixed);

    public static bool HasMultipartAlternativeContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Multipart.Alternative);

    public static bool HasMultipartRelatedContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Multipart.Related);

    public static bool HasImageSvgXmlContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Image.SvgXml);

    public static bool HasImageGifContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Image.Gif);

    public static bool HasImageJpegContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Image.Jpeg);

    public static bool HasImagePngContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Image.Png);

    public static bool HasImageTiffContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Image.Tiff);

    public static bool HasImageVndMicrosoftIconContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Image.VndMicrosoftIcon);

    public static bool HasImageXIconContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Image.XIcon);

    public static bool HasImageVndDjvuContentType(this HttpContent content) =>
        content.IsContentTypeEqualsTo(HttpContentType.Image.VndDjvu);

    private static bool IsContentTypeEqualsTo(this HttpContent content, string contentType) =>
        content.Headers.ContentType.MediaType == contentType;
}