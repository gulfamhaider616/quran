using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quran.Models
{
    public class AskQuestionDO
    {
        public int AskQuestionID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Subject { get; set; }
        public string Explanation { get; set; }
        public int Publish { get; set; }
    }

    public class AyatDO
    {
        public int ChapterID { get; set; }
        public int VerseID { get; set; }
        public string AyatText { get; set; }
        public string EnglishTranslation { get; set; }
        public string UrduTranslation { get; set; }
        public string IndonasianTranslation { get; set; }
        public string TurkishTranslation { get; set; }
        public string ChineseTranslation { get; set; }
        public string SpanishTranslation { get; set; }
        public int TotalVerses { get; set; }
    }

    public class BookDO
    {
        public int BookID { get; set; }
        public string BookTilte { get; set; }
        public string AutherName { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string BookType { get; set; }
        public string Detail { get; set; }
    }

    public class ContactUsDO
    {
        public string ContactTopic { get; set; }
        public string ContactEmail { get; set; }
        public string ContactMessage { get; set; }
        public string ContactDate { get; set; }
    }

    public class EmailDO
    {
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }
        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        public string Message { get; set; }
    }

    public class FeedbackDO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Message { get; set; }
    }

    public class LessonsContract
    {
        public List<VideoLessonDO> list { get; set; }
        public VideoLessonDO Lesson { get; set; }
    }

    public class RegistrationDO
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string SkypeID { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Classes { get; set; }
        public string Days { get; set; }
        public string FeasibleTime { get; set; }
        public string RegistrationDate { get; set; }
        public int Scheduled { get; set; }
        public string FirstLanguage { get; set; }
        public string UpdatedRecord { get; set; }

        public string ScheduledDays { get; set; }
        public string ClassTime { get; set; }
        public string TutorName { get; set; }
        public string Description { get; set; }
    }

    public class ScheduleDO
    {
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public int Classes { get; set; }
        public string Days { get; set; }
        public string ClassTime { get; set; }
        public string TutorName { get; set; }
        public string Description { get; set; }
    }

    public class StudentListDO
    {
        public List<RegistrationDO> StudentList { get; set; }
        public int TotalRecords { get; set; }
    }

    public class SuraDetailContract
    {
        public SuraNamesDO SuraDetail { get; set; }
        public List<AyatDO> AyatList { get; set; }
        public List<SuraNamesDO> SuraList { get; set; }
        public string trans { get; set; }
    }

    public class SuraNamesDO
    {
        public int ChapterID { get; set; }
        public string SuraName { get; set; }
        public string EnglishName { get; set; }
        public int TotalVerses { get; set; }
    }

    public class UserDO
    {
        public string Name { get; set; }
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BookmarkId { get; set; }
        public string ChapterID { get; set; }
        public string VerseID { get; set; }
    }

    public class VideoLessonDO
    {
        public int LessonID { get; set; }
        public string LessonName { get; set; }
        public string LessonLink { get; set; }
    }

    public class AdminUserDO
    {
        public int Id { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
    }

    public class FeaturedVerseDO
    {
        public int ChapterID { get; set; }
        public int VerseID { get; set; }
        public string Arabic { get; set; }
        public string English { get; set; }
        public string Urdu { get; set; }
        public string SuraName { get; set; }
        public string EnglishName { get; set; }
        public int Total { get; set; }
    }
}
