using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebCore.Configuration;

public static class SwaggerConfiguration
{
    public static void UseAppSwagger(this WebApplication webApplication)
    {

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        webApplication.UseSwagger();
        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        webApplication.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
    }
    public static void UseSwagger(this WebApplicationBuilder builder)
    {

        // Register the Swagger generator, defining 1 or more Swagger documents
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            c.ResolveConflictingActions(x => x.First());
        });

    }
}