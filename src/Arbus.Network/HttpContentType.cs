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

    public const string ImageSvgXmlContentType = "image/svg+xml";
    
    public static class Multipart
    {
        public const string FormData = "multipart/form-data";
        public const string Mixed = "multipart/mixed";
        public const string Alternative = "multipart/alternative";
        public const string Related  = "multipart/related ";
    }
}