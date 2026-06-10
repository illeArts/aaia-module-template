using Aaia.Shared.Contracts.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Aaias.Modules.Template;

/// <summary>
/// Entry point for the Template module.
/// Rename "Template" to your module name throughout the project.
/// </summary>
public sealed class TemplateModule : IAaiaModule
{
    public string Id          => "template";               // lowercase, no spaces
    public string DisplayName => "Template Module";
    public string Version     => "1.0.0";
    public string Description => "A starting point for AAIA modules.";

    public void AddServices(IServiceCollection services)
    {
        // Register your services here.
        // Example:
        // services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<TemplateService>();
    }

    public void MapRoutes(WebApplication app)
    {
        // All routes MUST be under /api/modules/{your-id}/
        app.MapGet("/api/modules/template/hello", (TemplateService svc) =>
            svc.Greet("World"));

        app.MapGet("/api/modules/template/status", () =>
            new { status = "ok", module = Id, version = Version });
    }
}
