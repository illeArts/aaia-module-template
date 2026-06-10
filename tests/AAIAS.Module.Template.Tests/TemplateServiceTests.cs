using AAIA.Shared.Contracts.Dev;
using Aaias.Modules.Template;
using Xunit;

namespace AAIAS.Module.Template.Tests;

public class TemplateServiceTests
{
    private readonly TemplateService _sut = new(NullDevDiagnosticsBus.Instance);

    [Fact]
    public void Greet_ReturnsMessageContainingName()
    {
        var result = _sut.Greet("AAIA");
        Assert.Contains("AAIA", result);
    }

    [Fact]
    public void Greet_ReturnsNonEmptyString()
    {
        var result = _sut.Greet("test");
        Assert.False(string.IsNullOrWhiteSpace(result));
    }
}
