using System.Reflection;
using Microsoft.OpenApi.Models;

namespace MoviesAPI.DependencyInjection;

public static class SetupSwagger
{
    public static IServiceCollection AddAndConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                    { Title = "MoviesAPI", Version = "v1", Contact = new OpenApiContact { Name = "Matheus Diniz" } });

            // Include XML comments into swagger documentation
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            // Add security definition
            options.AddSecurityDefinition("SimpleToken", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Hardcoded token authentication (The token is \"apisecret\")",
            });

            // Add requirement
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "SimpleToken"
                        }
                    },
                    new string[] { }
                }
            });
        });
        return services;
    }
}