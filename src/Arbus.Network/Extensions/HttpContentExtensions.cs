namespace Arbus.Network.Extensions;

public static class HttpContentExtensions
{
    public static bool IsApplicationJson(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Application.Json);

    public static bool IsApplicationText(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Application.Text);

    public static bool IsApplicationProtobuf(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Application.Protobuf);

    public static bool IsApplicationProblemJson(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Application.ProblemJson);

    public static bool IsTextPlain(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Text.Plain);

    public static bool IsMultipartFormData(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Multipart.FormData);

    public static bool IsMultipartMixed(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Multipart.Mixed);

    public static bool IsMultipartAlternative(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Multipart.Alternative);

    public static bool IsMultipartRelated(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Multipart.Related);

    public static bool IsImageSvgXml(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Image.SvgXml);

    public static bool IsImageGif(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Image.Gif);

    public static bool IsImageJpeg(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Image.Jpeg);

    public static bool IsImagePng(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Image.Png);

    public static bool IsImageTiff(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Image.Tiff);

    public static bool IsImageVndMicrosoftIcon(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Image.VndMicrosoftIcon);

    public static bool IsImageXIcon(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Image.XIcon);

    public static bool IsImageVndDjvu(this HttpContent content) =>
        IsRequiredContentType(content, HttpContentType.Image.VndDjvu);

    private static bool IsRequiredContentType(HttpContent content, string contentType) =>
        content.Headers.ContentType?.MediaType == contentType;
}