namespace Arbus.Network;

public static class HttpContentType
{
    public static class Application
    {
        public const string Protobuf = "application/x-protobuf";
        public const string Json = "application/json";
        public const string ProblemJson = "application/problem+json";
        public const string Text = "application/text";
    }

    public static class Text
    {
        public const string Plain = "text/plain";
    }

    public static class Image
    {
        public const string SvgXml = "image/svg+xml";
        public const string Gif = "image/gif";
        public const string Jpeg = "image/jpeg";
        public const string Png = "image/png";
        public const string Tiff = "image/tiff";
        public const string VndMicrosoftIcon = "image/vnd.microsoft.icon";
        public const string XIcon = "image/x-icon";
        public const string VndDjvu = "image/vnd.djvu";
    }

    public static class Multipart
    {
        public const string FormData = "multipart/form-data";
        public const string Mixed = "multipart/mixed";
        public const string Alternative = "multipart/alternative";
        public const string Related  = "multipart/related ";
    }
}