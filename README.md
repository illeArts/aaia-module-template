# AAIA Module Template

[![Build](https://github.com/illeArts/aaia-module-template/actions/workflows/build.yml/badge.svg)](https://github.com/illeArts/aaia-module-template/actions/workflows/build.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

**Startpunkt für AAIA-Module.** Klonen, umbenennen, implementieren.

> **Kompatibilität:** Dieses Template funktioniert mit AAIAS V2 (minHostVersion 2.0.0) und V3 (minHostVersion 3.0.0).  
> V3 ist additive — V2-Module laufen weiterhin ohne Änderungen auf V2-Hosts.

---

## Schnellstart

### Option A — GitHub (empfohlen)

Click **"Use this template"** auf GitHub, Repository `AAIAS.Module.YourName` benennen, klonen.

### Option B — Direkt klonen

```bash
git clone https://github.com/illeArts/aaia-module-template AAIAS.Module.YourName
cd AAIAS.Module.YourName
rm -rf .git && git init
```

---

## Umbenennen

Alle Vorkommen von `Template` / `template` durch deinen Modulnamen ersetzen:

| Suchen | Ersetzen durch |
|--------|---------------|
| `Template` | `YourModule` (PascalCase) |
| `template` | `your-module` (lowercase) |
| `com.example.template` | `com.yourcompany.yourmodule` |

Dateien umbenennen:
- `src/AAIAS.Module.Template/` → `src/AAIAS.Module.YourModule/`
- `AAIAS.Module.Template.sln` → `AAIAS.Module.YourModule.sln`
- `TemplateModule.cs` → `YourModuleModule.cs`
- `aaia-extension.json` → alle Felder aktualisieren

---

## Struktur

```
aaia-module-template/
├── src/
│   └── AAIAS.Module.Template/
│       ├── AAIAS.Module.Template.csproj   ← referenziert AAIA.Shared.Contracts
│       ├── TemplateModule.cs              ← IAaiaModule Implementation
│       ├── TemplateService.cs             ← Beispiel-Service
│       └── aaia-extension.json           ← Modul-Manifest
├── tests/
│   └── AAIAS.Module.Template.Tests/
│       └── TemplateServiceTests.cs
├── .github/workflows/build.yml
└── README.md
```

---

## AAIA.Shared.Contracts — Versionsempfehlung

| AAIAS-Zielversion | PackageReference |
|:-----------------:|-----------------|
| V2 (2.x) | `Version="2.1.0"` |
| V3 (3.x) | `Version="2.2.0"` |

**Wichtig:** Pinne immer auf eine exakte Version. Keine Wildcards (`*`).

```xml
<!-- Für V2-Hosts (Standard — breite Kompatibilität): -->
<PackageReference Include="AAIA.Shared.Contracts" Version="2.1.0" />

<!-- Für V3-Features (Security Pairing DTOs, V3 Enums/DTOs, etc.): -->
<PackageReference Include="AAIA.Shared.Contracts" Version="2.2.0" />
```

Version 2.2.0 ist **voll abwärtskompatibel** zu 2.1.0 — nur additiv ergänzt.

---

## Manifest — V2 vs. V3

### V2-kompatibles Manifest (Standard, empfohlen für neue Module)

```json
{
  "id":             "com.example.template",
  "displayName":    "Template Module",
  "version":        "1.0.0",
  "host":           "AAIAS",
  "kind":           "Module",
  "assembly":       "AAIAS.Module.Template.dll",
  "description":    "A starting point for AAIA modules.",
  "author":         "Your Name",
  "permissions":    [],
  "supportedPlatforms": ["all"],
  "minHostVersion": "2.0.0"
}
```

### V3-Manifest (für Module, die V3-Features nutzen)

```json
{
  "id":             "com.example.template",
  "displayName":    "Template Module",
  "version":        "1.0.0",
  "host":           "AAIAS",
  "kind":           "Module",
  "assembly":       "AAIAS.Module.Template.dll",
  "description":    "A starting point for AAIA modules.",
  "author":         "Your Name",
  "permissions":    [],
  "supportedPlatforms": ["all"],
  "minHostVersion": "3.0.0",
  "trustLevel":     "Community",
  "riskLevel":      "Low"
}
```

> `minHostVersion: "3.0.0"` nur setzen, wenn dein Modul explizit V3-Features benötigt.  
> V3 ist noch nicht offiziell released — für maximale Kompatibilität bei `2.0.0` bleiben.

---

## `IAaiaModule` — Interface

```csharp
public string Id          { get; }   // eindeutig, lowercase
public string DisplayName { get; }
public string Version     { get; }   // SemVer
public string Description { get; }

void AddServices(IServiceCollection services);  // DI-Registrierung
void MapRoutes(WebApplication app);              // HTTP-Endpunkte
```

---

## V3-Auth (nur für V3-Hosts)

Auf V3-Hosts werden Endpunkte mit `[RequireRole]` gesichert:

```csharp
using Aaias.Core.V3.Auth;

// Nur für eingeloggte Nutzer (ReadOnly oder höher):
[RequireRole(AaiaUserRole.ReadOnly)]
[HttpGet("/api/modules/my-module/data")]
public IActionResult GetData() { ... }

// Nur für Admins:
[RequireRole(AaiaUserRole.Admin)]
[HttpPost("/api/modules/my-module/admin-action")]
public IActionResult AdminAction() { ... }
```

Ohne `[RequireRole]` sind Endpunkte öffentlich erreichbar.

---

## Manifest-Felder

| Feld | Pflicht | Beschreibung |
|------|:-------:|--------------|
| `id` | ✅ | Eindeutige Reverse-Domain-ID |
| `displayName` | ✅ | Lesbarer Name |
| `version` | ✅ | SemVer |
| `host` | ✅ | Immer `"AAIAS"` |
| `kind` | ✅ | `"Module"` oder `"Plugin"` |
| `assembly` | ✅ | DLL-Dateiname |
| `permissions` | ✅ | Leeres Array wenn keine Berechtigungen |
| `supportedPlatforms` | ✅ | `["all"]` oder `["windows","linux","mac"]` |
| `minHostVersion` | — | Minimum AAIAS-Version |
| `trustLevel` | V3 | `Official`/`Verified`/`Community`/`Unverified` |
| `riskLevel` | V3 | `Low`/`Medium`/`High`/`Critical` |

---

## Regeln

- Routen müssen unter `/api/modules/{your-id}/` liegen
- Kein direkter Datenbank-Zugriff (über DI-Services gehen)
- Alle Permissions im Manifest deklarieren — nicht deklarierte werden abgelehnt
- `duki.autonomous` ist keine gültige Permission — niemals verwenden

---

## Links

- [AAIA.Shared.Contracts auf NuGet](https://www.nuget.org/packages/AAIA.Shared.Contracts)
- [SDK Repository](https://github.com/illeArts/aaia-sdk)
- [Developer Docs](https://github.com/illeArts/aaia-developer-docs)

---

## License

MIT
