using Microsoft.AspNetCore.Authentication;
using MoviesAPI.Auth;

namespace MoviesAPI.DependencyInjection;

public static class SetupAuthentication
{
    public static IServiceCollection AddTokenAuthorization(this IServiceCollection services)
    {
        services.AddAuthentication("SimpleToken")
            .AddScheme<AuthenticationSchemeOptions, TokenAuthorizationHandler>("SimpleToken", null);
        return services;
    }
}