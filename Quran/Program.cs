using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quran.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Allow large video uploads (up to 500 MB) for video lessons.
const long MaxUploadBytes = 524288000; // 500 MB
builder.Services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = MaxUploadBytes;
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = MaxUploadBytes;
});

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Index";
        options.AccessDeniedPath = "/Admin/Index";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

builder.Services.AddAuthorization();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

DbConfig.ConnectionString = builder.Configuration.GetConnectionString("myConnectionString");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("quran_reading", "quran_reading/{ChapterID?}/{trans?}",
    new { controller = "Quran", action = "SuraDetail" });
app.MapControllerRoute("online_quran_reading", "online_quran_reading",
    new { controller = "Quran", action = "GetAllSuraNames" });
app.MapControllerRoute("Quran_Teacher", "Quran_Teacher",
    new { controller = "QuranTeacher", action = "QuranTeacherHome" });
app.MapControllerRoute("UserTrust", "UserTrust",
    new { controller = "Home", action = "UserTrust" });
app.MapControllerRoute("Instructor_Terms", "Instructor_Terms",
    new { controller = "Home", action = "TermsForInstructor" });
app.MapControllerRoute("Students_Terms", "Students_Terms",
    new { controller = "Home", action = "TermsForStudentsAndParents" });
app.MapControllerRoute("Forum", "Forum",
    new { controller = "Forum", action = "ForumHomePage" });
app.MapControllerRoute("Registration", "Registration",
    new { controller = "Home", action = "Registration" });
app.MapControllerRoute("Check_Schedule", "Check_Schedule",
    new { controller = "Home", action = "GetStudentScheduleByID" });
app.MapControllerRoute("Due_e_Qunoot", "Due_e_Qunoot",
    new { controller = "Home", action = "Due_e_Qunoot" });
app.MapControllerRoute("Janaza", "Namaz_e_Janaza",
    new { controller = "Home", action = "ReadNamazJanaza" });
app.MapControllerRoute("Darood", "Darood",
    new { controller = "Home", action = "ReadDarood" });
app.MapControllerRoute("Duain", "Masnoon_Duain",
    new { controller = "Home", action = "ReadDuain" });
app.MapControllerRoute("Six_Kalmas", "Six_Kalmas",
    new { controller = "Home", action = "ReadKalmas" });
app.MapControllerRoute("Video_Lessons", "Video_Lessons",
    new { controller = "Home", action = "QuraniLesson" });
app.MapControllerRoute("About", "About",
    new { controller = "Home", action = "About" });
app.MapControllerRoute("Contact", "Contact",
    new { controller = "Home", action = "Contact" });
app.MapControllerRoute("Namaz", "Namaz",
    new { controller = "Home", action = "ReadNamaz" });
app.MapControllerRoute("online_quran_teacher_section_1", "online_quran_teacher_section_1",
    new { controller = "QuranTeacher", action = "Section_1" });
app.MapControllerRoute("online_quran_teacher_section_2", "online_quran_teacher_section_2",
    new { controller = "QuranTeacher", action = "Section_2" });
app.MapControllerRoute("online_quran_teacher_section_3", "online_quran_teacher_section_3",
    new { controller = "QuranTeacher", action = "Section_3" });
app.MapControllerRoute("online_quran_teacher_section_4", "online_quran_teacher_section_4",
    new { controller = "QuranTeacher", action = "Section_4" });
app.MapControllerRoute("online_quran_teacher_section_5", "online_quran_teacher_section_5",
    new { controller = "QuranTeacher", action = "Section_5" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
