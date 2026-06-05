# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

ASP.NET Core MVC web application (.NET 10) for "Learn Quran with Tutor" — an online Quran learning platform. It serves public Quran reading (multi-language translations), tutor/teacher pages, a Q&A forum, a books library, video lessons, student registration/scheduling, and an admin back office. The single project lives under [Quran/](Quran/); the repo root holds the solution and SQL database scripts.

## Build & Run

All commands run from the repo root (`d:\quran`).

```powershell
dotnet build Quran.slnx                       # build
dotnet run --project Quran                     # run (Development profile, https://localhost:53620)
dotnet watch --project Quran run               # run with hot reload
```

There is **no test project, linter config, or CI build** in this repo. The `.github/` folder contains no workflows. Do not invent test commands.

## Database

The app talks to **SQL Server LocalDB**, database `LFQDB`, via the connection string `myConnectionString` in [Quran/appsettings.json](Quran/appsettings.json). The connection string is read once at startup in [Quran/Program.cs](Quran/Program.cs) and stored in the static `DbConfig.ConnectionString` ([Quran/DataAccess/DbConfig.cs](Quran/DataAccess/DbConfig.cs)) — the DataAccess layer reads it from there, not from DI.

To provision the database, restore one of the root `.sql` scripts into LocalDB (e.g. `LFQDB_localdb.sql` / `LFQDB_dbo_only.sql`). These dumps include the schema **and the stored procedures the app depends on**. `_restore_log.txt` is generated output and is gitignored.

**Almost all data access goes through stored procedures**, not inline SQL — `DBConnection.DbSqlConnection(name)` opens a connection with `CommandType.StoredProcedure` and the string argument is the **stored procedure name**. If you add or change a data method, the matching stored procedure must exist in `LFQDB`. Inline `CommandType.Text` SQL is the rare exception (see `QuranDA.GetFeaturedVerse`).

## Architecture

Classic four-layer MVC, manually wired (no repository interfaces, no DI for business/data classes — they are `new`'d directly):

```
Controller  →  *BA (Business)  →  *DA (DataAccess)  →  stored procedure (SQL Server)
   View      ←  Model/DO/Contract objects  ←──────────┘
```

- **[Quran/Controllers/](Quran/Controllers/)** — thin controllers, one per area: `Home`, `Quran`, `QuranTeacher`, `Tutor`, `User`, `Forum`, `Books`, `Admin`. They `new` a `*BA` and return a View or `RedirectToAction`.
- **[Quran/Business/](Quran/Business/)** (`*BA.cs`) — calls the matching `*DA`, then maps the returned `DataSet` into typed model objects by reading `DataTable`/`DataRow` columns with `dr.Field<T>("ColumnName")`. **Column-name strings here must match the stored-procedure result set exactly.**
- **[Quran/DataAccess/](Quran/DataAccess/)** (`*DA.cs`) — executes a stored procedure via `DBConnection`, adds `SqlParameter`s, fills a `DataSet`, and returns it untyped. `DBConnection` ([Quran/DataAccess/DBConnection.cs](Quran/DataAccess/DBConnection.cs)) is the only place that constructs `SqlConnection`/`SqlCommand`.
- **[Quran/Models/Models.cs](Quran/Models/Models.cs)** — every model lives in this one file. Naming convention: `*DO` = a single row/entity, `*Contract` = a composite/view-model bundling several lists (e.g. `SuraDetailContract` carries the sura, its ayat, the full sura list, and the selected translation).
- **[Quran/Views/](Quran/Views/)** — Razor views, foldered per controller, plus `Shared/_Layout.cshtml` (public) and `Shared/_AdminLayout.cshtml` (admin). `_ViewImports.cshtml` globally imports `Quran.Models` and `Quran.Business`, so views may call BA classes directly.

### Cross-cutting setup (all in [Quran/Program.cs](Quran/Program.cs))

- **Routing**: a large block of named `MapControllerRoute` entries gives SEO-friendly URLs (e.g. `/quran_reading/{ChapterID}/{trans}`, `/online_quran_reading`, `/Quran_Teacher`, `/Forum`) before the `{controller=Home}/{action=Index}/{id?}` default. When adding a public page with a custom URL, add a named route here.
- **Auth**: cookie authentication only, 60-min expiry. Login is custom — `AdminController.VerifyAdmin` checks credentials via `AdminBA` and calls `HttpContext.SignInAsync`. Protected admin actions are marked `[Authorize]`; `LoginPath`/`AccessDeniedPath` both point to `/Admin/Index`. There is no ASP.NET Identity.
- **Sessions** enabled (60-min idle), used for transient state in `Admin`/`User`/`Home` flows.
- **Uploads**: Kestrel and form limits are raised to **500 MB** to allow video-lesson uploads. Uploaded videos are written under `Quran/wwwroot/assets/Videos/` (gitignored as user content) via `IWebHostEnvironment` injected into `AdminController`. JSON property naming is left unchanged (`PropertyNamingPolicy = null`).

## Conventions when extending

- Adding a feature usually means touching four files in lockstep: `Controllers/XController.cs`, `Business/XBA.cs`, `DataAccess/XDA.cs`, plus a new stored procedure in `LFQDB`, and a Razor view under `Views/X/`.
- Add new model/DO/contract classes to the single `Models/Models.cs` file, following the `*DO` / `*Contract` naming.
- `ImplicitUsings` and `Nullable` are **disabled** — add explicit `using` directives and don't rely on nullable reference annotations.
- The only NuGet dependency is `Microsoft.Data.SqlClient`.
- **Keep comments minimal and purposeful.** Don't add noise comments that merely restate the code (e.g. `// loop through rows`), commented-out dead code, or TODO/scaffolding leftovers. Remove any such existing comments when you touch the surrounding code. Only keep comments that explain *why* something non-obvious is done (like the existing 500 MB upload-limit note in `Program.cs`).
