using AAIA.Shared.Contracts.Dev;

namespace Aaias.Modules.Template;

/// <summary>
/// Example service — replace with your own logic.
/// </summary>
public sealed class TemplateService(IDevDiagnosticsBus bus)
{
    public string Greet(string name)
    {
        bus.Publish(DevDiagnosticsEvent.Info(
            source:      "template",
            componentId: "template",
            message:     $"Greet called for '{name}'"
        ));

        return $"Hello, {name}! Template module is running.";
    }
}
