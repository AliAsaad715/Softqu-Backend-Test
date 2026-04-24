using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace Softqu.API.Extensions
{
    public static class ScalarExtensions
    {
        internal static IServiceCollection AddScalarConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();

            // Register OpenAPI v1
            services.AddOpenApi("v1", options =>
            {
                options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;

                // Call our helper to inject OAuth2 security settings
                //options.UseOAuth2Authentication(config);

                // Add API Info
                options.AddDocumentTransformer((document, context, ct) =>
                {
                    document.Info = new OpenApiInfo
                    {
                        Title = "Softqu API",
                        Version = "v1",
                        Description = "Business API for Softqu platform."
                    };
                    return Task.CompletedTask;
                });
            });

            return services;
        }

        internal static WebApplication UseScalarDashboard(this WebApplication app)
        {
            app.MapOpenApi(); // Generates the JSON

            app.MapScalarApiReference("/docs", options =>
            {
                // UI Customization
                options.WithTitle("Softqu API Documentation").AddPreferredSecuritySchemes("oauth2");

                // Setup OAuth Client for the "Test" button
                options.AddAuthorizationCodeFlow("oauth2", flow =>
                {
                    flow.ClientId = "softqu_scalar"; // Must match IdentityServer Config
                    flow.SelectedScopes = new[] { "openid", "profile", "roles", "api1" };
                    flow.Pkce = Pkce.Sha256;
                });
            });
            return app;
        }
    }
}
