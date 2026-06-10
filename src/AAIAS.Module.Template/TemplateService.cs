using AAIA.Shared.Contracts.Dev;

namespace Aaias.Modules.Template;

/// <summary>
/// Example service — replace with your own logic.
/// </summary>
public sealed class TemplateService(IDevDiagnosticsBus bus)
{
    public string Greet(string name)
    {
        bus.Publish(new DevDiagnosticsEvent(
            Source:   "template",
            Message:  $"Greet called for '{name}'",
            Severity: DevEventSeverity.Info
        ));

        return $"Hello, {name}! Template module is running.";
    }
}
