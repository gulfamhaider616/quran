using Quran.DataAccess;
using Quran.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quran.Business
{
    public class AdminBA
    {
        public string VerifyAdmin(string adminemail,string adminpassword)
        {
            DataSet dataset = new AdminDA().VerifyAdmin(adminemail, adminpassword);
            string AdminName = "";
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    AdminName = r["AdminName"].ToString();
                }
            }
            return AdminName;
        }

        public StudentListDO GetAllStudents()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            DataSet dataset = new AdminDA().GetAllStudents();
            try
            {
                if (dataset.Tables.Count > 0)
                {
                    foreach (DataRow dr in dataset.Tables[0].Rows)
                    {
                        RegistrationDO registration = new RegistrationDO();
                        registration.StudentID = dr.Field<string>("StudentID");
                        registration.StudentName = dr.Field<string>("Name");
                        registration.FatherName = dr.Field<string>("FatherName");
                        registration.PhoneNumber = dr.Field<string>("PhoneNumber");
                        registration.Email = dr.Field<string>("Email");
                        registration.SkypeID = dr.Field<string>("SkypeID");
                        registration.DateOfBirth = dr.Field<string>("DateOfBirth");
                        registration.City = dr.Field<string>("City");
                        registration.Gender = dr.Field<string>("Gender");
                        registration.Country = dr.Field<string>("Country");
                        registration.Classes = dr.Field<int>("Classes");
                        if (dr.Field<int>("Classes") == 7)
                        {
                            registration.Days = "All";
                        }
                        else
                        {
                            try
                            {
                                string[] days = (dr.Field<string>("DaysName") ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                string result = "";
                                foreach (var day in days)
                                {
                                    result += (day.Length >= 3 ? day.Substring(0, 3) : day) + ", ";
                                }
                                registration.Days = result.Length >= 2 ? result.Substring(0, result.Length - 2) : result;
                            }
                            catch (Exception ex)
                            {
                                registration.Days = "**Not Valid**" + dr.Field<string>("DaysName").ToString();
                            }
                        }
                        try
                        {
                            string date = dr.Field<DateTime>("RegistrationDate").ToString();
                            registration.RegistrationDate = date.Substring(0, date.Length - 11);
                        }
                        catch (Exception ex)
                        {
                            registration.RegistrationDate = "Not Valid";
                        }
                        registration.FeasibleTime = dr.Field<string>("FeasibleTime");
                        registration.Scheduled = dr.Field<int>("IsScheduled");
                        list.Add(registration);
                    }
                    if (dataset.Tables[1].Rows.Count > 0)
                    {
                        DataRow r = dataset.Tables[1].Rows[0];
                        studentList.TotalRecords = Convert.ToInt32(r["TotalRecords"]);
                    }
                }
                studentList.StudentList = list;
                return studentList;
            }
            catch(Exception ex)
            {
                return studentList;
            }
        }

        public StudentListDO GetUnscheduledStudents()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            DataSet dataset = new AdminDA().GetUnscheduledStudents();
            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    RegistrationDO registration = new RegistrationDO();
                    registration.StudentID = dr.Field<string>("StudentID");
                    registration.StudentName = dr.Field<string>("Name");
                    registration.FatherName = dr.Field<string>("FatherName");
                    registration.PhoneNumber = dr.Field<string>("PhoneNumber");
                    registration.Email = dr.Field<string>("Email");
                    registration.SkypeID = dr.Field<string>("SkypeID");
                    registration.DateOfBirth = dr.Field<string>("DateOfBirth");
                    registration.City = dr.Field<string>("City");
                    registration.Gender = dr.Field<string>("Gender");
                    registration.Country = dr.Field<string>("Country");
                    registration.Classes = dr.Field<int>("Classes");
                    if (dr.Field<int>("Classes") == 7)
                    {
                        registration.Days = "All";
                    }
                    else
                    {
                        string[] days = (dr.Field<string>("DaysName") ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        string result = "";
                        foreach (var day in days)
                        {
                            result += (day.Length >= 3 ? day.Substring(0, 3) : day) + ", ";
                        }
                        registration.Days = result.Length >= 2 ? result.Substring(0, result.Length - 2) : result;
                    }
                    registration.FeasibleTime = dr.Field<string>("FeasibleTime");
                    string date = dr.Field<DateTime>("RegistrationDate").ToString();
                    registration.RegistrationDate = date.Substring(0, date.Length - 11);
                    registration.Scheduled = dr.Field<int>("IsScheduled");
                    list.Add(registration);
                }
                if (dataset.Tables[1].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[1].Rows[0];
                    studentList.TotalRecords = Convert.ToInt32(r["TotalRecords"]);
                }
            }
            studentList.StudentList = list;
            return studentList;
        }


        public StudentListDO GetScheduledStudents()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            DataSet dataset = new AdminDA().GetScheduledStudents();
            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    RegistrationDO registration = new RegistrationDO();
                    registration.StudentID = dr.Field<string>("StudentID");
                    registration.StudentName = dr.Field<string>("Name");
                    registration.PhoneNumber = dr.Field<string>("PhoneNumber");
                    registration.SkypeID = dr.Field<string>("SkypeID");
                    registration.Gender = dr.Field<string>("Gender");
                    registration.Country = dr.Field<string>("Country");
                    registration.Classes = dr.Field<int>("Classes");
                    if (dr.Field<int>("Classes") == 7)
                    {
                        registration.Days = "All";
                    }
                    else
                    {
                        registration.Days = dr.Field<string>("DaysName");
                    }
                    registration.ClassTime = dr.Field<string>("ClassTime");
                    registration.TutorName = dr.Field<string>("TutorName");
                    registration.Description = dr.Field<string>("Description");
                    list.Add(registration);
                }
                if (dataset.Tables[1].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[1].Rows[0];
                    studentList.TotalRecords = Convert.ToInt32(r["TotalRecords"]);
                }
            }
            studentList.StudentList = list;
            return studentList;
        }

        public StudentListDO GetTodaySchedule()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            DataSet dataset = new AdminDA().GetTodaySchedule();
            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    RegistrationDO registration = new RegistrationDO();
                    registration.StudentID = dr.Field<string>("StudentID");
                    registration.StudentName = dr.Field<string>("Name");
                    registration.PhoneNumber = dr.Field<string>("PhoneNumber");
                    registration.SkypeID = dr.Field<string>("SkypeID");
                    registration.Gender = dr.Field<string>("Gender");
                    registration.Country = dr.Field<string>("Country");
                    registration.Classes = dr.Field<int>("Classes");
                    if (dr.Field<int>("Classes") == 7)
                    {
                        registration.Days = "All";
                    }
                    else
                    {
                        string[] days = (dr.Field<string>("DaysName") ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        string result = "";
                        foreach (var day in days)
                        {
                            result += (day.Length >= 3 ? day.Substring(0, 3) : day) + ", ";
                        }
                        registration.Days = result.Length >= 2 ? result.Substring(0, result.Length - 2) : result;
                    }
                    registration.ClassTime = dr.Field<string>("ClassTime");
                    registration.TutorName = dr.Field<string>("TutorName");
                    registration.Description = dr.Field<string>("Description");
                    list.Add(registration);
                }
                if (dataset.Tables[1].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[1].Rows[0];
                    studentList.TotalRecords = Convert.ToInt32(r["TotalRecords"]);
                }
            }
            studentList.StudentList = list;
            return studentList;
        }


        public int SaveSchedule(ScheduleDO schedule)
        {
            DataSet dataset = new AdminDA().SaveSchedule(schedule);
            int scheduleID = 0;
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    scheduleID = Convert.ToInt32(r["ScheduleID"]);
                }
            }
            return scheduleID;
        }

        public string ChangeSchedule(ScheduleDO schedule)
        {
            DataSet dataset = new AdminDA().ChangeSchedule(schedule);
            string StudentID="";
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    StudentID = r["StudentID"].ToString();
                }
            }
            return StudentID;
        }

        public List<ContactUsDO> GetAllContactUs()
        {
            List<ContactUsDO> list = new List<ContactUsDO>();
            DataSet dataset = new AdminDA().GetAllContactUs();
            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    ContactUsDO registration = new ContactUsDO();
                    registration.ContactTopic = dr.Field<string>("ContactTopic");
                    registration.ContactEmail = dr.Field<string>("ContactEmail");
                    registration.ContactMessage = dr.Field<string>("ContactMessage");
                    registration.ContactDate = dr.Field<DateTime>("ContactDate").ToString();
                    list.Add(registration);
                }
            }
            return list;
        }
        public int DeleteFeedback(int feedbackID)
        {
            return new AdminDA().DeleteFeedback(feedbackID);
        }

        public RegistrationDO StudentPreview(string studentID)
        {
            DataSet dataset = new AdminDA().StudentPreview(studentID);
            RegistrationDO registration = new RegistrationDO();
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dataset.Tables[0].Rows[0];
                    registration.StudentID = dr.Field<string>("StudentID");
                    registration.StudentName = dr.Field<string>("Name");
                    registration.FatherName = dr.Field<string>("FatherName");
                    registration.PhoneNumber = dr.Field<string>("PhoneNumber");
                    registration.Email = dr.Field<string>("Email");
                    registration.SkypeID = dr.Field<string>("SkypeID");
                    registration.DateOfBirth = dr.Field<string>("DateOfBirth");
                    registration.City = dr.Field<string>("City");
                    registration.Gender = dr.Field<string>("Gender");
                    registration.Country = dr.Field<string>("Country");
                    registration.Classes = dr.Field<int>("Classes");
                    registration.Days = dr.Field<string>("DaysName");
                    registration.FirstLanguage = dr.Field<string>("FirstLanguage");
                    registration.UpdatedRecord = dr.Field<DateTime?>("UpdatedRecord").ToString();
                    registration.FeasibleTime = dr.Field<string>("FeasibleTime");
                    string date = dr.Field<DateTime>("RegistrationDate").ToString();
                    registration.RegistrationDate = date.Substring(0, date.Length - 11);
                    registration.Scheduled = dr.Field<int>("IsScheduled");
                   
                    //schedule information
                    registration.ScheduledDays= dr.Field<string>("scheduledDays");
                    registration.TutorName= dr.Field<string>("TutorName");
                    registration.ClassTime = dr.Field<string>("scheduledClassTime");
                    registration.Description = dr.Field<string>("Description");
                }
            }
            return registration;
        }

        public string AddBook(BookDO book)
        {
            DataSet dataset = new AdminDA().AddBook(book);
            string BookID = string.Empty;
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    BookID = r["BookID"].ToString();
                }
            }
            return BookID;
        }

        public int ChangeBook(BookDO book)
        {
            return new AdminDA().ChangeBook(book);

        }

        public List<BookDO> GetAllBooks()
        {
            List<BookDO> list = new List<BookDO>();
            DataSet dataset = new AdminDA().GetAllBooks();
            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    BookDO book = new BookDO();
                    book.BookID = dr.Field<int>("BookID");
                    book.BookTilte = dr.Field<string>("BookTilte");
                    book.AutherName = dr.Field<string>("AuthorName");
                    book.ImagePath = dr.Field<string>("ImagePath");
                    book.FilePath = dr.Field<string>("FilePath");
                    book.BookType = dr.Field<string>("BookType");
                    book.Detail = dr.Field<string>("Detail");
                    if (!string.IsNullOrWhiteSpace(book.ImagePath))
                    {
                        string[] imgPath = book.ImagePath.Split('/');
                        book.ImageName = imgPath[imgPath.Length - 1];
                    } 
                    
                    
                    if (!string.IsNullOrWhiteSpace(book.FilePath))
                    {
                        string[] filePath = book.FilePath.Split('/');
                        book.FileName = filePath[filePath.Length - 1];
                    }

                    list.Add(book);
                }
            }
            return list;
        }
        public int DeleteBook(int BookID)
        {
            return new AdminDA().DeleteBook(BookID);
        }

    }
}

