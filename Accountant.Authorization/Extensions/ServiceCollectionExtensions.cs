using Accountant.Authorization.Token.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accountant.Authorization.Extensions {
    public static class ServiceCollectionExtension {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            var tokenSettings = new TokenSettings();
            configurationSection.Bind(tokenSettings);

            var tokenProvider = new TokenProvider(tokenSettings);

            services.AddSingleton<ITokenProvider>(tokenProvider);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = tokenProvider.ValidationParameters;
                });
                
            services.AddAuthorization(auth => {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            return services;
        }
    }
}