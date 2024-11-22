using System.Collections.Immutable;
using DarkLink.Web.WebFinger.Server;
using DarkLink.Web.WebFinger.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebFinger.Server.OidcDiscovery;

// ReSharper disable once UnusedType.Global
public static class WebFingerOidcExtension
{
    // ReSharper disable once UnusedMember.Global
    public static IServiceCollection AddOidcWebFinger(this IServiceCollection services, OidcIssuer oidcIssuer)
    {
        services.AddWebFinger(new OidcResourceDescriptorProvider(oidcIssuer));
        return services;
    }

    // ReSharper disable once UnusedMember.Global
    public static IServiceCollection AddOidcWebFinger(this IServiceCollection services,
        Func<IServiceProvider, OidcIssuer> createIssuer)
    {
        services.AddWebFinger(sp => new OidcResourceDescriptorProvider(createIssuer(sp)));
        return services;
    }

    private class OidcResourceDescriptorProvider : IResourceDescriptorProvider
    {
        private readonly OidcIssuer issuer;

        public OidcResourceDescriptorProvider(OidcIssuer issuer)
        {
            this.issuer = issuer;
        }

        public Task<JsonResourceDescriptor?> GetResourceDescriptorAsync(
            Uri resource,
            IReadOnlyList<string> relations,
            HttpRequest request,
            CancellationToken cancellationToken) => Task.FromResult<JsonResourceDescriptor?>(new JsonResourceDescriptor(
            resource,
            ImmutableList<Uri>.Empty,
            ImmutableDictionary<Uri, string?>.Empty,
            relations.Count == 0 || relations.Contains(OidcConstants.OPENID_ISSUER_RELATION)
                ? ImmutableList.Create(new Link(
                    OidcConstants.OPENID_ISSUER_RELATION,
                    default,
                    issuer.Endpoint,
                    ImmutableDictionary<string, string>.Empty,
                    ImmutableDictionary<Uri, string?>.Empty))
                : ImmutableList<Link>.Empty
        ));
    }
}