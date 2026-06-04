using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LearnFreeQuran.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "quran_reading",
                url: "quran_reading/{ChapterID}/{trans}",
                defaults: new { controller = "Quran", action = "SuraDetail", ChapterID = UrlParameter.Optional, trans= UrlParameter.Optional }
            );

            routes.MapRoute(
                "online_quran_reading",
                "online_quran_reading",
                new { controller = "Quran", action = "GetAllSuraNames" }
                );

            routes.MapRoute(
                "Quran_Teacher",
                "Quran_Teacher",
                new {controller= "QuranTeacher",action= "QuranTeacherHome" }
                );

            routes.MapRoute(
                "UserTrust",
                "UserTrust",
                new { controller = "Home", action = "UserTrust" }
                );

            routes.MapRoute(
                "Instructor_Terms",
                "Instructor_Terms",
                new { controller = "Home", action = "TermsForInstructor" }
                );

            routes.MapRoute(
                "Students_Terms",
                "Students_Terms",
                new { controller = "Home", action = "TermsForStudentsAndParents" }
                );

            routes.MapRoute(
                "Forum",
                "Forum",
                new { controller = "Forum", action = "ForumHomePage" }
                );

            routes.MapRoute(
                "Registration",
                "Registration",
                new { controller = "Home", action = "Registration" }
                );

            routes.MapRoute(
                "Check_Schedule",
                "Check_Schedule",
                new {controller="Home",action= "GetStudentScheduleByID" }
                );

            routes.MapRoute(
                "Due_e_Qunoot",
                "Due_e_Qunoot",
                new {controller="Home",action= "Due_e_Qunoot" }
                );

            routes.MapRoute(
                "Janaza",
                "Namaz_e_Janaza",
                new {controller="Home",action= "ReadNamazJanaza" }
                );

            routes.MapRoute(
                "Darood",
                "Darood",
                new {controller="Home",action= "ReadDarood" }
                );

            routes.MapRoute (
                "Duain",
                "Masnoon_Duain",
                new {controller="Home",action= "ReadDuain" }

            );

            routes.MapRoute(
              "Six_Kalmas",
              "Six_Kalmas",
              new { controller = "Home", action = "ReadKalmas" }

          );

            routes.MapRoute(
               "About", // Route name
               "About", // URL with parameters
               new { controller = "Home", action = "About" }// Parameter defaults
             );

            routes.MapRoute(
               "Contact",
               "Contact",
               new { controller = "Home", action = "Contact" }
             );

            routes.MapRoute(
             "Namaz",
             "Namaz",
             new { controller = "Home", action = "ReadNamaz" }
           );

            #region Quran_Lesson_Routing

            routes.MapRoute(
              "online_quran_teacher_section_1",
              "online_quran_teacher_section_1",
              new { controller = "QuranTeacher", action = "Section_1" }
            );
            routes.MapRoute(
              "online_quran_teacher_section_2",
              "online_quran_teacher_section_2",
              new { controller = "QuranTeacher", action = "Section_2" }
            );
            routes.MapRoute(
              "online_quran_teacher_section_3",
              "online_quran_teacher_section_3",
             new { controller = "QuranTeacher", action = "Section_3" }
            );
            routes.MapRoute(
              "online_quran_teacher_section_4",
              "online_quran_teacher_section_4",
              new { controller = "QuranTeacher", action = "Section_4" }
            );
            routes.MapRoute(
              "online_quran_teacher_section_5",
              "online_quran_teacher_section_5",
              new { controller = "QuranTeacher", action = "Section_5" }
            );

            #endregion


           // routes.MapRoute(
           //  "Learn_Free_Quran",
           //  "Learn_Free_Quran",
           //  new { controller = "Home", action = "Index" }
           //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
