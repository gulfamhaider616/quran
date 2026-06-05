# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

ASP.NET Core MVC web application (**.NET 10**) for "Learn Quran with Tutor" — a free online Quran learning platform. It serves:

- **Public site** — home page (dynamic featured verse + Islamic-date/prayer-time widgets), online Quran reading with multi-language translation, audio and verse bookmarking, a structured **Quran Teacher** course (5 sections) with a **practice test**, an Islamic **books library** (read/download), **video lessons** (YouTube or uploaded files), a Q&A **forum**, student **registration/scheduling**, fee structure, and Islamic-basics pages (Namaz, Kalmas, Duain, Darood, etc.).
- **Admin back office** — students & class scheduling, books, video lessons, forum moderation (publish/unpublish/delete), contact messages, feedback, and admin-user management.

The single project lives under [Quran/](Quran/); the repo root holds the solution (`Quran.slnx`) and SQL database scripts. The public site and admin panel share a green (`#38ab1e`) / blue (`#2a486c`) theme. Site-wide list **search** and a reusable **confirmation modal** are cross-cutting UI features (see *Front-end*).

## Build & Run

All commands run from the repo root (`d:\quran`).

```powershell
dotnet build Quran.slnx                       # build
dotnet run --project Quran                     # run (Development profile, https://localhost:53620)
dotnet watch --project Quran run               # run with hot reload
```

There is **no test project, linter config, or CI build** in this repo. The `.github/` folder contains no workflows. Do not invent test commands.

When verifying manually, the admin login is at `/Admin/Index`; sign in via `/Admin/VerifyAdmin?adminname=<email>&adminpassword=<pwd>` before hitting protected pages. NuGet dependencies are **`Microsoft.Data.SqlClient`** and **`Dapper`**. `ImplicitUsings` and `Nullable` are **disabled** — use explicit `using`s and don't rely on nullable reference annotations.

## Project map

- **Controllers** ([Quran/Controllers/](Quran/Controllers/)): `Home`, `Quran`, `QuranTeacher`, `Tutor`, `User`, `Forum`, `Books`, `Admin`.
- **Business** ([Quran/Business/](Quran/Business/)): `AdminBA`, `QuranBA`, `ForumBA`, `RegistrationBA`, `UserBA`.
- **DataAccess** ([Quran/DataAccess/](Quran/DataAccess/)): `AdminDA`, `QuranDA`, `ForumDA`, `RegistrationDA`, `UserDA`, plus the shared `Db` and `RowExtensions` helpers.
- **Models**: one file, [Quran/Models/Models.cs](Quran/Models/Models.cs).
- **Views** ([Quran/Views/](Quran/Views/)): foldered per controller (`Admin`, `Books`, `Forum`, `Home`, `Quran`, `QuranTeacher`, `Tutor`, `User`) + `Shared` (`_Layout.cshtml` public, `_AdminLayout.cshtml` admin, partials).
- **Static assets** ([Quran/wwwroot/](Quran/wwwroot/)): `assets/css/app.css` (public theme) + `assets/css/admin.css` (admin theme), `Scripts/` (authored + vendor JS), `assets/Books/` (book covers + files), `assets/Videos/` (uploaded video lessons, gitignored), `assets/AyatAudios/`, `assets/VideoLessons/`, lesson icons, images.

## Database

The app talks to **SQL Server LocalDB**, database `LFQDB`, via the connection string `myConnectionString` in [Quran/appsettings.json](Quran/appsettings.json). It is read once at startup in [Quran/Program.cs](Quran/Program.cs) and stored in `Db.ConnectionString` ([Quran/DataAccess/Db.cs](Quran/DataAccess/Db.cs)) — the DataAccess layer reads it from there, not from DI.

The database uses the **`dbo` schema only** (legacy per-user schemas were collapsed into `dbo`). To provision it, restore one of the root `.sql` scripts into LocalDB (e.g. `LFQDB_localdb.sql` / `LFQDB_dbo_only.sql`). These dumps include the schema **and the stored procedures the app depends on**. `_restore_log.txt` is generated output and is gitignored.

**Key tables:** `AdminUser`, `Registration`, `Schedule`, `Books`, `VideoLesson`, `Forum`, `Feedback`, `ContactUs`, `USERS`, `USERBOOKMARK`, `Quran`/`QuranEnglish`/`QuranUrdu`/`ur_kanzuliman` (verse text + translations), `SuraNames`.

### Data access uses Dapper

The whole DataAccess layer runs on **Dapper**. The connection **and** the Dapper call boilerplate live in one file, [Db.cs](Quran/DataAccess/Db.cs): `Db.Connection()` is the only place a `SqlConnection` is constructed, and the static `Db` helpers wrap every open-connection/query pattern. `*DA` methods are therefore one-liners that call a helper — **do not write `using (new SqlConnection(...))` in a DA; use the `Db` helper.**

- **Stored procedures** — `Db.QueryProc(name, param)` (row list), `Db.QueryProcSingle(name, param)` (first row), `Db.ExecuteProc(name, param)` (rows affected). Most read/write paths (students, schedules, books, sura/verse lookups, forum questions, contact/feedback) use the existing procs in `LFQDB`.
- **Inline parameterized SQL** — `Db.Query(sql, param)`, `Db.QuerySingle(sql, param)`, `Db.Execute(sql, param)`, `Db.ExecuteScalar<T>(sql, param)` (default `CommandType.Text`) against `dbo.*`. Used where adding a proc was undesirable: admin-user CRUD ([AdminDA.cs](Quran/DataAccess/AdminDA.cs)), video-lesson save/delete ([RegistrationDA.cs](Quran/DataAccess/RegistrationDA.cs)), `Publish`/`UnPublishQuestionByAdmin` ([ForumDA.cs](Quran/DataAccess/ForumDA.cs)), `GetFeaturedVerse` ([QuranDA.cs](Quran/DataAccess/QuranDA.cs)).
- **Two-result-set procs** — `Db.QueryProcTwo(name, param)` returns a `(First, Second)` tuple of row lists via `QueryMultiple` (guarded by `grid.IsConsumed`). Used by the four student procs (data + a `TotalRecords` count set) and by `GetSuraByID` (sura header + ayat list).

`*DA` methods return rows as **`IDictionary<string, object>`** (single row) or **`List<IDictionary<string, object>>`** (Dapper's dynamic `DapperRow`), or a scalar `int` for writes. The `*BA` layer maps those rows into typed `*DO` objects using the helpers in [RowExtensions.cs](Quran/DataAccess/RowExtensions.cs): **`row.Get<T>("Column")`** (mirrors `DataRow.Field<T>` — case-insensitive lookup, returns null for reference/`Nullable<T>`, throws on null for non-nullable value types) and **`row.Str("Column")`** (mirrors `row["Column"].ToString()` — `""` for null/DBNull). These helpers kept the existing business transforms (day-name truncation, date formatting, path splitting) byte-for-byte identical through the Dapper migration.

**Guidance:** for a new data method, follow the same shape — Dapper `Query`/`Execute`/`ExecuteScalar` (proc or inline `dbo` SQL), return `IDictionary`/`List<IDictionary>`/scalar, map in the BA with `Get<T>`/`Str`. Always parameterize via an anonymous object (`new { Id = id }`) — never concatenate user input. Prefer inline `dbo` SQL when it avoids a proc migration; if you rely on a stored procedure, confirm it exists in `LFQDB` (a missing proc throws at runtime). There is **no `DBConnection`/`DbConfig` wrapper and no `DataSet`/`SqlDataAdapter`** anymore — don't reintroduce them.

### Admin users & auth

Admin login and the **Manage Admins** CRUD page operate on `dbo.AdminUser` (`Id`, `AdminName`, `AdminEmail`, `AdminPassword`). `VerifyAdmin` compares the password **in plaintext** — passwords are stored unhashed (a known legacy limitation; preserve current login behavior unless asked to add hashing). An already-authenticated admin who hits `/Admin/Index` is redirected to the dashboard (`GetAllStudents`), so the login page only ever appears when genuinely logged out.

## Architecture

Classic four-layer MVC, manually wired (no repository interfaces, no DI for business/data classes — they are `new`'d directly):

```
Controller  →  *BA (Business)  →  *DA (DataAccess via Dapper)  →  stored procedure OR inline dbo SQL (SQL Server)
   View      ←  Model/DO/Contract objects  ←──────────┘
```

- **Controllers** — thin; they `new` a `*BA` and return a View or `RedirectToAction`. Validation/branching errors are surfaced to views via `TempData` (e.g. `AdminSuccess`/`AdminError`, `VideoSuccess`/`VideoError`, `BookSuccess`/`BookError`). `AdminController` takes `IWebHostEnvironment` for file uploads.
- **Business** (`*BA.cs`) — calls the matching `*DA`, then maps Dapper rows into typed models with `dr.Get<T>`/`dr.Str`. **Column-name strings must match the result set exactly.**
- **DataAccess** (`*DA.cs`) — one-liner methods over the `Db` helpers.
- **Models** ([Models.cs](Quran/Models/Models.cs)) — every model in one file. `*DO` = a single row/entity (`BookDO`, `VideoLessonDO`, `AdminUserDO`, `FeaturedVerseDO`, `RegistrationDO`, `AskQuestionDO`, …); `*Contract` = a composite view-model bundling several lists (e.g. `SuraDetailContract`).
- **Views** — `_ViewImports.cshtml` globally imports `Quran.Models` and `Quran.Business`, so views may call BA classes directly.

### Cross-cutting setup (all in [Quran/Program.cs](Quran/Program.cs))

- **Routing**: a large block of named `MapControllerRoute` entries gives SEO-friendly URLs (e.g. `/quran_reading/{ChapterID}/{trans}`, `/online_quran_reading`, `/Quran_Teacher`, `/online_quran_teacher_section_1..5`, `/Practice_Test`, `/Forum`, `/Video_Lessons`, `/Registration`) before the `{controller=Home}/{action=Index}/{id?}` default. When adding a public page with a custom URL, add a named route here.
- **Auth**: cookie authentication only, 60-min expiry. Login is custom (`AdminController.VerifyAdmin` → `HttpContext.SignInAsync`). Protected admin actions are `[Authorize]`; `LoginPath`/`AccessDeniedPath` point to `/Admin/Index`. No ASP.NET Identity. Logout uses `SignOutAsync` then `RedirectToAction("Index")`.
- **Sessions** enabled (60-min idle), used for transient state in `Admin`/`User`/`Home` flows (e.g. logged-in user name and current bookmark for the navbar "Goto Bookmark" link).
- **Uploads**: Kestrel and `FormOptions` limits raised to **500 MB** for video-lesson uploads. JSON property naming left unchanged (`PropertyNamingPolicy = null`).

## Features worth knowing

- **Online Quran reading** ([Views/Quran/SuraDetail.cshtml](Quran/Views/Quran/SuraDetail.cshtml)) — verses with per-ayah audio and a **Bookmark** button. Bookmarks are stored per user (`USERBOOKMARK`) as `"<ChapterID>-<VerseID>"`; the verse `div` id matches, so the navbar **Goto Bookmark** link routes via the `#<id>` fragment. `highlightBookmark()` highlights the bookmarked verse both on bookmark-click and on arrival via the fragment.
- **Quran Teacher course + Practice Test** — five lesson sections; `Section_5` and the course home link to **`/Practice_Test`** ([Views/QuranTeacher/PracticeTest.cshtml](Quran/Views/QuranTeacher/PracticeTest.cshtml)), a 10-question MCQ self-assessment scored entirely client-side (correct/wrong/unanswered tally + percentage; ≥85% = pass/certificate-eligible). Styled with `.pt-*` classes; no DB.
- **Video lessons** can be a **YouTube link or an uploaded file**. The single `dbo.VideoLesson.Link` column stores either a URL or a local path like `/assets/Videos/<guid>.ext`; code decides by checking the `/assets/Videos/` prefix. Uploads (mp4/webm/ogg/mov/m4v) go under `wwwroot/assets/Videos/` (gitignored) via `IWebHostEnvironment`; deleting a lesson removes its file. Admin adds via a modal ([Views/Admin/VideoLessons.cshtml](Quran/Views/Admin/VideoLessons.cshtml)); public renders a YouTube embed or HTML5 `<video>` ([Views/Home/QuraniLesson.cshtml](Quran/Views/Home/QuraniLesson.cshtml)).
- **Books library** — admin manages books on one page ([Views/Admin/GetAllBooks.cshtml](Quran/Views/Admin/GetAllBooks.cshtml)) with an **Add New Book** button and a single Add/Edit **modal** (multipart; cover image + PDF). Edits preserve the existing image/file when no new upload is provided.
- **Forum moderation** — questions have `IsPublish`. Admin can **Publish** (`UnPublish` page) and **Unpublish** (`Publish` page) and **Delete**, all via icon buttons calling AJAX in `Forum.js` (`PublishQuestion`/`UnPublishQuestion`/`DeleteQuestions`). The public *Ask a Question* form posts via AJAX and uses themed `.fr-modal` popups.
- **Home featured verse** is pulled dynamically from the Quran tables via inline SQL (`GetFeaturedVerse`).
- **Dashboard widgets** (Islamic Hijri date + local prayer times) on the home page are client-side: `Intl` Islamic calendar + the Aladhan API via geolocation.

## Front-end: theming & reusable client components

CSS lives in **two single stylesheets**, not per-view `<style>` blocks: [app.css](Quran/wwwroot/assets/css/app.css) (public) and [admin.css](Quran/wwwroot/assets/css/admin.css) (admin). **Avoid inline styles and per-view `<style>` blocks** — add classes to the appropriate stylesheet. `app.css` is linked *after* Bootstrap so its theme rules win. Vendor CSS stays separate. The public navbar (`.nv*`) and footer (`.ftr*`) are custom and compact; forms use a cohesive input/button style (rounded, green focus ring, hover states).

Reusable JavaScript lives in [wwwroot/Scripts/](Quran/wwwroot/Scripts/) (authored: `MainFile.js`, `MainFileAdmin.js`, `Forum.js`, `searchFilter.js`, `confirmModal.js`) and is wired through the layouts:

- **`searchFilter.js`** — site-wide list search (both layouts).
  - *Admin:* auto-enhances every `<table class="rest">` (only on `.admin-body`), injecting a search box into the page's `.adm-toolbar` (or creating one) so **search + the page's action button share one line**, then filters `<tbody>` rows live.
  - *Public:* any `<input data-search-target="#container" data-search-items=".item" data-search-empty="#emptyEl">` filters matching items by text (books, surah list, forum, video lessons, feedback). An optional `data-search="..."` on an item scopes what it matches.
- **`confirmModal.js`** + `#confirmModal` markup in `_AdminLayout.cshtml` — one reusable themed confirmation modal for **every** admin delete (replacing browser `confirm()`). Declarative: add `data-confirm="…"` + `data-href="/Admin/DeleteX?Id=…"` to a link. Programmatic (AJAX): `confirmAction('message', function () { /* delete */ })`.

**Reusable CSS building blocks:**
- Admin (`admin.css`): `.adm-modal` (+ `-sm`/`-wide`/`-head`/`-head-danger`/`-body`/`-foot`) for popups; `.adm-toolbar` (search + action button row); `.tbl-search` (injected search); `.adm-btn`/`-primary`/`-ghost`/`-danger` buttons; `.adm-alert-success`/`-error` for `TempData`; `.adm-form-row`/`.adm-field-error` for modal forms; `.rest td .btn.btn-success`/`-info`/`-danger`/`-warning` for table action icon-buttons. Admin **add/edit flows use a single modal reused for both** (Manage Admins, Video Lessons, Books) — the form `action` and labels are swapped in JS, edit values come from row `data-*` attributes.
- Public (`app.css`): `.fr-modal*` (forum popups), `.pt-*` (practice test), `.agree-row` (themed inline Terms/Privacy checkbox used by Registration, Ask-a-Question, New Tutor), `.sd-*` (Quran reading, incl. `.sd-bookmarked` highlight), `.qr-*`/`.ib-*`/`.vl-*`/`.hm-*`/`.qt-*`/`.ql-*` per section.

## Conventions when extending

- A new data-backed feature typically touches: `Controllers/XController.cs`, `Business/XBA.cs`, `DataAccess/XDA.cs`, a Razor view under `Views/X/`, and styles in `app.css`/`admin.css`. Use **Dapper** via the `Db` helpers (proc or inline `dbo` SQL) and map rows in the BA with `Get<T>`/`Str`; prefer inline `dbo` SQL when it avoids a proc migration.
- Add new model/DO/contract classes to the single `Models/Models.cs` file, following `*DO` / `*Contract` naming.
- For an admin **list** page, render a `<table class="rest">` — search is added automatically; put the page action in an `.adm-toolbar`. For a public list, add a `data-search-target` input.
- For admin **add/edit**, reuse one themed `.adm-modal` swapped between add and edit in JS (see Books/Admins/Video Lessons). For any **delete**, use the reusable confirm modal — never the browser `confirm()`.
- Put styles in the shared stylesheets (no inline styles / `<style>` blocks); keep the green/blue theme.
- **Keep comments minimal and purposeful.** No noise comments that restate code, no commented-out dead code, no TODO/scaffolding leftovers — remove such when you touch surrounding code. Keep only comments explaining *why* something non-obvious is done.

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
