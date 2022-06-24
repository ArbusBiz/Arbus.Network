namespace Arbus.Network.Application
{
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
    }
}