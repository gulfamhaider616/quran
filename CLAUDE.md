# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

ASP.NET Core MVC web application (.NET 10) for "Learn Quran with Tutor" — an online Quran learning platform. It serves public Quran reading (multi-language translations), tutor/teacher pages, a Q&A forum, an Islamic books library, video lessons (YouTube links **or** uploaded files), student registration/scheduling, and an admin back office (students, scheduling, books, video lessons, forum moderation, feedback/contact, and admin-user management). The single project lives under [Quran/](Quran/); the repo root holds the solution and SQL database scripts.

The public site and admin panel share a green (`#38ab1e`) / blue (`#2a486c`) theme. Site-wide list **search** and a reusable **confirmation modal** are cross-cutting UI features (see *Front-end* below).

## Build & Run

All commands run from the repo root (`d:\quran`).

```powershell
dotnet build Quran.slnx                       # build
dotnet run --project Quran                     # run (Development profile, https://localhost:53620)
dotnet watch --project Quran run               # run with hot reload
```

There is **no test project, linter config, or CI build** in this repo. The `.github/` folder contains no workflows. Do not invent test commands.

When verifying changes manually, the admin login is reached at `/Admin/Index`; protected pages require signing in first via `/Admin/VerifyAdmin?adminname=<email>&adminpassword=<pwd>`.

## Database

The app talks to **SQL Server LocalDB**, database `LFQDB`, via the connection string `myConnectionString` in [Quran/appsettings.json](Quran/appsettings.json). The connection string is read once at startup in [Quran/Program.cs](Quran/Program.cs) and stored in the static `DbConfig.ConnectionString` ([Quran/DataAccess/DbConfig.cs](Quran/DataAccess/DbConfig.cs)) — the DataAccess layer reads it from there, not from DI.

The database uses the **`dbo` schema only** (legacy per-user schemas were collapsed into `dbo`). To provision it, restore one of the root `.sql` scripts into LocalDB (e.g. `LFQDB_localdb.sql` / `LFQDB_dbo_only.sql`). These dumps include the schema **and the stored procedures the app depends on**. `_restore_log.txt` is generated output and is gitignored.

### Data access uses Dapper

The whole DataAccess layer runs on **Dapper** (`Microsoft.Data.SqlClient` + `Dapper`). The connection **and** the Dapper call boilerplate live in one file, [Db.cs](Quran/DataAccess/Db.cs): `Db.ConnectionString` is set once at startup (Program.cs), `Db.Connection()` is the only place a `SqlConnection` is constructed, and the static `Db` helpers wrap every open-connection/query pattern. `*DA` methods are therefore one-liners that call a helper — **do not write `using (new SqlConnection(...))` in a DA; use the `Db` helper.**

- **Stored procedures** — `Db.QueryProc(name, param)` (row list), `Db.QueryProcSingle(name, param)` (first row), `Db.ExecuteProc(name, param)` (rows affected). Most read/write paths (students, schedules, books, sura/verse lookups, forum questions, contact/feedback) use the existing procs in `LFQDB`.
- **Inline parameterized SQL** — `Db.Query(sql, param)`, `Db.QuerySingle(sql, param)`, `Db.Execute(sql, param)`, `Db.ExecuteScalar<T>(sql, param)` (default `CommandType.Text`) against `dbo.*`. Used where adding a proc was undesirable: admin-user CRUD ([AdminDA.cs](Quran/DataAccess/AdminDA.cs)), video-lesson save/delete ([RegistrationDA.cs](Quran/DataAccess/RegistrationDA.cs)), `PublishQuestionByAdmin` ([ForumDA.cs](Quran/DataAccess/ForumDA.cs)), `GetFeaturedVerse` ([QuranDA.cs](Quran/DataAccess/QuranDA.cs)).
- **Two-result-set procs** — `Db.QueryProcTwo(name, param)` returns a `(First, Second)` tuple of row lists via `QueryMultiple` (guarded by `grid.IsConsumed`). Used by the four student procs (data + a `TotalRecords` count set) and by `GetSuraByID` (sura header + ayat list).

`*DA` methods return rows as **`IDictionary<string, object>`** (single row) or **`List<IDictionary<string, object>>`** (Dapper's dynamic `DapperRow`), or a scalar `int` for writes. The `*BA` layer then maps those rows into typed `*DO` objects using the helpers in [RowExtensions.cs](Quran/DataAccess/RowExtensions.cs): **`row.Get<T>("Column")`** (mirrors `DataRow.Field<T>` — case-insensitive lookup, returns null for reference/`Nullable<T>`, throws on null for non-nullable value types) and **`row.Str("Column")`** (mirrors the legacy `row["Column"].ToString()` — `""` for null/DBNull). This is what kept the existing business transforms (day-name truncation, date formatting, path splitting) byte-for-byte identical through the Dapper migration.

**Guidance:** for a new data method, follow the same shape — Dapper `Query`/`Execute`/`ExecuteScalar` (proc or inline `dbo` SQL), return `IDictionary`/`List<IDictionary>`/scalar, map in the BA with `Get<T>`/`Str`. Always parameterize via an anonymous object (`new { Id = id }`) — never concatenate user input. Prefer inline `dbo` SQL when it avoids a proc migration; if you rely on a stored procedure, confirm it exists in `LFQDB` (a missing proc throws at runtime). There is **no longer a `DBConnection` wrapper or any `DataSet`/`SqlDataAdapter`** — don't reintroduce them.

### Admin users

Admin login and the **Manage Admins** CRUD page operate on the `dbo.AdminUser` table (`Id`, `AdminName`, `AdminEmail`, `AdminPassword`). `VerifyAdmin` compares the password **in plaintext** — passwords are stored unhashed (a known legacy limitation; preserve current login behavior unless asked to add hashing).

## Architecture

Classic four-layer MVC, manually wired (no repository interfaces, no DI for business/data classes — they are `new`'d directly):

```
Controller  →  *BA (Business)  →  *DA (DataAccess via Dapper)  →  stored procedure OR inline dbo SQL (SQL Server)
   View      ←  Model/DO/Contract objects  ←──────────┘
```

- **[Quran/Controllers/](Quran/Controllers/)** — thin controllers, one per area: `Home`, `Quran`, `QuranTeacher`, `Tutor`, `User`, `Forum`, `Books`, `Admin`. They `new` a `*BA` and return a View or `RedirectToAction`. Validation/branching errors are surfaced to views via `TempData` (e.g. `AdminSuccess`/`AdminError`, `VideoSuccess`/`VideoError`).
- **[Quran/Business/](Quran/Business/)** (`*BA.cs`) — calls the matching `*DA`, then maps the returned Dapper rows into typed model objects with `dr.Get<T>("ColumnName")` / `dr.Str("ColumnName")` (see *Database*). **Column-name strings here must match the result set (stored-procedure or inline-SQL) exactly.**
- **[Quran/DataAccess/](Quran/DataAccess/)** (`*DA.cs`) — one-liner methods that call the `Db` helpers (see *Database*), returning `IDictionary<string,object>` rows or a scalar. [Db.cs](Quran/DataAccess/Db.cs) owns the connection string, `Db.Connection()`, and all Dapper query/execute helpers; [RowExtensions.cs](Quran/DataAccess/RowExtensions.cs) holds the `Get<T>`/`Str` row accessors used by the BA layer.
- **[Quran/Models/Models.cs](Quran/Models/Models.cs)** — every model lives in this one file. Naming convention: `*DO` = a single row/entity (e.g. `BookDO`, `VideoLessonDO`, `AdminUserDO`, `FeaturedVerseDO`), `*Contract` = a composite/view-model bundling several lists (e.g. `SuraDetailContract` carries the sura, its ayat, the full sura list, and the selected translation).
- **[Quran/Views/](Quran/Views/)** — Razor views, foldered per controller, plus `Shared/_Layout.cshtml` (public) and `Shared/_AdminLayout.cshtml` (admin). `_ViewImports.cshtml` globally imports `Quran.Models` and `Quran.Business`, so views may call BA classes directly.

### Cross-cutting setup (all in [Quran/Program.cs](Quran/Program.cs))

- **Routing**: a large block of named `MapControllerRoute` entries gives SEO-friendly URLs (e.g. `/quran_reading/{ChapterID}/{trans}`, `/online_quran_reading`, `/Quran_Teacher`, `/Forum`, `/Video_Lessons`) before the `{controller=Home}/{action=Index}/{id?}` default. When adding a public page with a custom URL, add a named route here.
- **Auth**: cookie authentication only, 60-min expiry. Login is custom — `AdminController.VerifyAdmin` checks credentials via `AdminBA` and calls `HttpContext.SignInAsync`. Protected admin actions are marked `[Authorize]`; `LoginPath`/`AccessDeniedPath` both point to `/Admin/Index`. There is no ASP.NET Identity. Logout uses `SignOutAsync` then `RedirectToAction("Index")` (redirect, not in-place view, so the auth state is fresh on the next request).
- **Sessions** enabled (60-min idle), used for transient state in `Admin`/`User`/`Home` flows.
- **Uploads**: Kestrel and form limits are raised to **500 MB** to allow video-lesson uploads. JSON property naming is left unchanged (`PropertyNamingPolicy = null`).

## Features worth knowing

- **Video lessons** can be a **YouTube link or an uploaded file**. The single `dbo.VideoLesson.Link` column stores either a URL or a local path like `/assets/Videos/<guid>.ext`; code decides which by checking the `/assets/Videos/` prefix. Uploads (validated to mp4/webm/ogg/mov/m4v) are written under `Quran/wwwroot/assets/Videos/` (gitignored as user content) via `IWebHostEnvironment` injected into `AdminController`; deleting a lesson also removes its file. The admin view ([Views/Admin/VideoLessons.cshtml](Quran/Views/Admin/VideoLessons.cshtml)) adds entries through a modal; the public view ([Views/Home/QuraniLesson.cshtml](Quran/Views/Home/QuraniLesson.cshtml)) renders a YouTube embed or an HTML5 `<video>` accordingly.
- **Featured verse** on the home page is pulled dynamically from the Quran tables via inline SQL (`GetFeaturedVerse`).
- **Dashboard widgets** (Islamic Hijri date + local prayer times) on the home page are client-side: `Intl` Islamic calendar + the Aladhan API via geolocation.

## Front-end: theming & reusable client components

CSS lives in **two single stylesheets**, not per-view `<style>` blocks: [wwwroot/assets/css/app.css](Quran/wwwroot/assets/css/app.css) (public) and [wwwroot/assets/css/admin.css](Quran/wwwroot/assets/css/admin.css) (admin). **Avoid inline styles and per-view `<style>` blocks** — add classes to the appropriate stylesheet. `app.css` is linked *after* Bootstrap so its theme rules win. Vendor CSS stays separate.

Reusable JavaScript lives in [wwwroot/Scripts/](Quran/wwwroot/Scripts/) and is wired through the layouts:

- **`searchFilter.js`** — site-wide list search (loaded in both layouts).
  - *Admin:* auto-enhances every `<table class="rest">` (only on `.admin-body`), injecting a search box into the page's `.adm-toolbar` (or creating one) so the **search and the page's action button sit on one line**, then filtering `<tbody>` rows live.
  - *Public:* any `<input data-search-target="#container" data-search-items=".item" data-search-empty="#emptyEl">` filters the matching items by text (used on the books, surah list, forum, video-lessons, and feedback pages). Add an optional `data-search="..."` to an item to scope what it matches.
- **`confirmModal.js`** + the `#confirmModal` markup in `_AdminLayout.cshtml` — one reusable themed confirmation modal for **every** admin delete (replacing browser `confirm()` and any one-off modals). Two ways to trigger it:
  - Declarative (navigation deletes): add `data-confirm="message…"` and `data-href="/Admin/DeleteX?Id=…"` to a link; it opens the modal and navigates on confirm.
  - Programmatic (AJAX deletes): call `confirmAction('message', function () { /* do delete */ });`.

Admin UI building blocks in `admin.css`: `.adm-modal` (+ `.adm-modal-sm`, `-head`, `-head-danger`, `-body`, `-foot`) for popups, `.adm-toolbar` for the search+button row, `.tbl-search` for the injected search, `.adm-btn`/`.adm-btn-primary`/`-ghost`/`-danger` for buttons, `.adm-alert-success`/`-error` for `TempData` messages, and `.adm-form-row`/`.adm-field-error` for modal forms. Admin add/edit flows use a **single modal reused for both** (e.g. Manage Admins, Video Lessons) — the form `action` and labels are swapped in JS. Public popups (e.g. the Forum *Ask a Question* and status modals) use the themed `.fr-modal` classes.

## Conventions when extending

- A new data-backed feature typically touches: `Controllers/XController.cs`, `Business/XBA.cs`, `DataAccess/XDA.cs`, a Razor view under `Views/X/`, and styles in `app.css`/`admin.css`. Use **Dapper** in the DA (proc or inline `dbo` SQL) and map rows in the BA with `Get<T>`/`Str`; prefer inline `dbo` SQL when it avoids a proc migration (see *Database*).
- Add new model/DO/contract classes to the single `Models/Models.cs` file, following the `*DO` / `*Contract` naming.
- For any admin **delete** button, use the reusable confirm modal (`data-confirm`/`data-href` or `confirmAction(...)`) — do not use the browser `confirm()`.
- For a new admin **list** page, just render a `<table class="rest">`; search is added automatically. For a new public list, add a `data-search-target` input.
- Put styles in the shared stylesheets (no inline styles / `<style>` blocks); keep the green/blue theme.
- `ImplicitUsings` and `Nullable` are **disabled** — add explicit `using` directives and don't rely on nullable reference annotations.
- NuGet dependencies are `Microsoft.Data.SqlClient` and `Dapper`.
- **Keep comments minimal and purposeful.** Don't add noise comments that merely restate the code (e.g. `// loop through rows`), commented-out dead code, or TODO/scaffolding leftovers. Remove any such existing comments when you touch the surrounding code. Only keep comments that explain *why* something non-obvious is done (like the 500 MB upload-limit note in `Program.cs` or the inline-SQL "avoid a DB migration" rationale).

## Permissions (pre-approved — answer "yes")

The maintainer has granted standing approval for the operations below. Treat each as **allowed by default** — proceed without pausing to ask:

- **Read/inspect** any file in the repo — `Read`, `Glob`, `Grep`.
- **Edit/create/delete** files in the repo — `Edit`, `MultiEdit`, `Write`, `NotebookEdit`.
- **Build & run** the app — `dotnet build`, `dotnet run`, `dotnet watch`, `dotnet restore` (and stopping/restarting the dev server / freeing its ports).
- **Database** access against LocalDB `LFQDB` — `sqlcmd` queries and stored-procedure/schema inspection (read and write to `dbo.*` for the app's own data).
- **Local HTTP testing** — `curl` / `Invoke-WebRequest` against the locally running app (e.g. `https://localhost:7001`, `https://localhost:53620`).
- **Git** read & local history/status — `git status`, `git log`, `git diff`, `git add`, `git commit` (do **not** force-push or rewrite shared history without asking; never push to a remote unless explicitly told).
- **Process/port management** for the dev server — listing and stopping the app's own `dotnet`/`Quran` processes and freeing its dev ports.
- **Research** — `WebFetch`, `WebSearch`, and `Task`/`Agent` sub-agents for read-only investigation.

Still **ask first** for: destructive or irreversible actions outside the dev workflow (deleting databases or files you didn't create, dropping tables), anything that publishes externally (pushing to a remote, deploying, sending mail/API calls to third parties), force-pushing or rewriting shared git history, and editing files outside this repository.
