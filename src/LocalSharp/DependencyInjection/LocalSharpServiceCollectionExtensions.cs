using LocalSharp;
using LocalSharp.Http;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

// In the Microsoft.Extensions.DependencyInjection namespace, like other AddXxx() extensions, so it shows up
// on IServiceCollection without an extra using
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Registers <see cref="LocalSharpClient"/> with an <see cref="IServiceCollection"/>.
/// </summary>
public static class LocalSharpServiceCollectionExtensions
{
    /// <summary>The name of the <see cref="HttpClient"/> registered with <c>IHttpClientFactory</c>.</summary>
    internal const string HttpClientName = "LocalSharp";

    /// <summary>
    /// Registers one <see cref="LocalSharpClient"/> as a singleton, also resolvable as <see cref="IChatClient"/>,
    /// that sends requests through an <see cref="HttpClient"/> from <c>IHttpClientFactory</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="HttpClient"/> has no timeout of its own (requests are bounded by
    /// <see cref="LocalSharpOptions.RequestTimeout"/>) and connects to all of a host's addresses at once, like the
    /// clients LocalSharp creates itself. Use the returned builder to add message handlers (logging, resilience,
    /// headers) or to replace the primary handler.
    /// </para>
    /// <para>
    /// The options are read once, when the client is first resolved; later changes are not picked up. Calling
    /// this method more than once adds further <paramref name="configure"/> steps to the same client.
    /// </para>
    /// </remarks>
    /// <param name="services">The service collection.</param>
    /// <param name="configure">Sets the client's options. <see cref="LocalSharpOptions.BaseUrl"/> is required.</param>
    /// <returns>The builder for the <see cref="HttpClient"/> the client uses.</returns>
    public static IHttpClientBuilder AddLocalSharp(this IServiceCollection services, Action<LocalSharpOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);

        services.Configure(configure);

        // A singleton, not a transient typed client: see decision 0012
        services.TryAddSingleton(static provider => new LocalSharpClient(
            provider.GetRequiredService<IHttpClientFactory>().CreateClient(HttpClientName),
            provider.GetRequiredService<IOptions<LocalSharpOptions>>().Value));
        services.TryAddSingleton<IChatClient>(static provider => provider.GetRequiredService<LocalSharpClient>());

        return services.AddHttpClient(HttpClientName)
            .ConfigureHttpClient(static http => http.Timeout = Timeout.InfiniteTimeSpan)
            .ConfigurePrimaryHttpMessageHandler(OwnedHttpClient.CreateHandler)
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
    }
}
