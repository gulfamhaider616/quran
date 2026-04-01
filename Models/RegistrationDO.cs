using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFreeQuran.Library
{
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
}
