# WebFinger.Server.OidcDiscovery
An implementation of [OpenID Connect Discovery 1.0](https://openid.net/specs/openid-connect-discovery-1_0.html) for [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-8.0)

## Table of Contents

- [WebFinger.Server.OidcDiscovery](#webfingerserveroidcdiscovery)
  - [Table of Contents](#table-of-contents)
  - [Installation](#installation)
  - [Usage](#usage)
  - [License](#license)

## Installation

See [WebFinger.Server.OidcDiscovery](https://www.nuget.org/packages/WebFinger.Server.OidcDiscovery).

## Usage

```csharp
var builder = WebApplication.CreateBuilder();
// ...
// e.g. with Keycloak (domain:my-custom-domain.com realm: my-custom-realm)
builder.Services.AddOidcWebFinger(new OidcIssuer(new Uri("https://my-custom-domain.com/realms/my-custom-realm")));
// ...
var app = builder.Build();
// ...
app.UseWebFinger();
// ...
app.Run();
```

> In general the URI must point to the root of the OIDC provider so that client can take advantage of the well known routes.
> For details see [https://openid.net/specs/openid-connect-discovery-1_0.html](https://openid.net/specs/openid-connect-discovery-1_0.html).

## License
[MIT](https://github.com/bluehands/WebFinger.Server.OidcDiscovery/blob/main/LICENSE)