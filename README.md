# AAIA Module Template

[![Build](https://github.com/illeArts/aaia-module-template/actions/workflows/build.yml/badge.svg)](https://github.com/illeArts/aaia-module-template/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

**Starting point for building AAIA modules.** Clone, rename, implement.

## How to use this template

### Option A — GitHub (recommended)

Click **"Use this template"** on GitHub, name your repo `AAIAS.Module.YourName`, clone it.

### Option B — Clone directly

```bash
git clone https://github.com/illeArts/aaia-module-template AAIAS.Module.YourName
cd AAIAS.Module.YourName
rm -rf .git && git init
```

## Rename the template

Replace all occurrences of `Template` / `template` with your module name:

| Find | Replace with |
|------|-------------|
| `Template` | `YourModule` (PascalCase) |
| `template` | `your-module` (lowercase) |
| `com.example.template` | `com.yourcompany.yourmodule` |

Files to rename:
- `src/AAIAS.Module.Template/` → `src/AAIAS.Module.YourModule/`
- `AAIAS.Module.Template.sln` → `AAIAS.Module.YourModule.sln`
- `TemplateModule.cs` → `YourModuleModule.cs`
- `TemplateService.cs` → (your services)
- `aaia-extension.json` → update all fields

## Structure

```
aaia-module-template/
├── src/
│   └── AAIAS.Module.Template/
│       ├── AAIAS.Module.Template.csproj   ← references AAIA.Shared.Contracts
│       ├── TemplateModule.cs              ← IAaiaModule implementation
│       ├── TemplateService.cs             ← example service
│       └── aaia-extension.json           ← module manifest
├── tests/
│   └── AAIAS.Module.Template.Tests/
│       └── TemplateServiceTests.cs
├── .github/workflows/build.yml
└── README.md
```

## What `IAaiaModule` requires

```csharp
public string Id          { get; }   // unique, lowercase
public string DisplayName { get; }
public string Version     { get; }   // SemVer
public string Description { get; }

void AddServices(IServiceCollection services);  // DI registration
void MapRoutes(WebApplication app);              // HTTP endpoints
```

## Manifest fields

| Field | Required | Description |
|-------|----------|-------------|
| `id` | ✅ | Unique reverse-domain ID |
| `displayName` | ✅ | Human-readable name |
| `version` | ✅ | SemVer |
| `host` | ✅ | Always `"AAIAS"` |
| `kind` | ✅ | `"Module"` or `"Plugin"` |
| `assembly` | ✅ | Your `.dll` filename |
| `permissions` | ✅ | Empty array if none needed |
| `supportedPlatforms` | ✅ | `["all"]` or `["windows","linux","mac"]` |
| `minHostVersion` | — | Minimum AAIAS version required |

## Rules

- Routes must be under `/api/modules/{your-id}/`
- No direct database access
- Declare all permissions in the manifest — undeclared permissions are rejected

## Links

- [AAIA.Shared.Contracts on NuGet](https://www.nuget.org/packages/AAIA.Shared.Contracts)
- [SDK Repository](https://github.com/illeArts/aaia-sdk)
- [Developer Docs](https://github.com/illeArts/aaia-developer-docs) *(coming soon)*

## License

MIT
