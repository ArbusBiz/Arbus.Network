namespace Arbus.Network.Extensions;

public static class HttpContentExtensions
{
    public static bool IsApplicationJson(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Application.Json);

    public static bool IsApplicationText(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Application.Text);

    public static bool IsApplicationProtobuf(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Application.Protobuf);

    public static bool IsApplicationProblemJson(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Application.ProblemJson);

    public static bool IsTextPlain(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Text.Plain);

    public static bool IsMultipartFormData(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Multipart.FormData);

    public static bool IsMultipartMixed(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Multipart.Mixed);

    public static bool IsMultipartAlternative(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Multipart.Alternative);

    public static bool IsMultipartRelated(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Multipart.Related);

    public static bool IsImageSvgXml(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Image.SvgXml);

    public static bool IsImageGif(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Image.Gif);

    public static bool IsImageJpeg(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Image.Jpeg);

    public static bool IsImagePng(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Image.Png);

    public static bool IsImageTiff(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Image.Tiff);

    public static bool IsImageVndMicrosoftIcon(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Image.VndMicrosoftIcon);

    public static bool IsImageXIcon(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Image.XIcon);

    public static bool IsImageVndDjvu(this HttpContent content) =>
        content.IsEqualsTo(HttpContentType.Image.VndDjvu);

    private static bool IsEqualsTo(this HttpContent content, string contentType) =>
        content.Headers.ContentType.MediaType == contentType;
}