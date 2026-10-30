namespace LocalSharp.Tests.Fakes;

internal static class TestClients
{
    public const string BaseUrl = "http://localhost:1234";

    public static LocalSharpClient Create(FakeHttpHandler handler, Action<LocalSharpOptions>? configure = null)
    {
        var options = new LocalSharpOptions { BaseUrl = BaseUrl };
        configure?.Invoke(options);
        return new LocalSharpClient(new HttpClient(handler), options);
    }
}
