using Quran.DataAccess;
using Quran.Models;
using System;
using System.Collections.Generic;

namespace Quran.Business
{
    public class AdminBA
    {
        public string VerifyAdmin(string adminemail, string adminpassword)
        {
            IDictionary<string, object> r = new AdminDA().VerifyAdmin(adminemail, adminpassword);
            return r == null ? "" : r.Str("AdminName");
        }

        public StudentListDO GetAllStudents()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            var data = new AdminDA().GetAllStudents();
            try
            {
                foreach (IDictionary<string, object> dr in data.Rows)
                {
                    RegistrationDO registration = new RegistrationDO();
                    registration.StudentID = dr.Get<string>("StudentID");
                    registration.StudentName = dr.Get<string>("Name");
                    registration.FatherName = dr.Get<string>("FatherName");
                    registration.PhoneNumber = dr.Get<string>("PhoneNumber");
                    registration.Email = dr.Get<string>("Email");
                    registration.SkypeID = dr.Get<string>("SkypeID");
                    registration.DateOfBirth = dr.Get<string>("DateOfBirth");
                    registration.City = dr.Get<string>("City");
                    registration.Gender = dr.Get<string>("Gender");
                    registration.Country = dr.Get<string>("Country");
                    registration.Classes = dr.Get<int>("Classes");
                    if (dr.Get<int>("Classes") == 7)
                    {
                        registration.Days = "All";
                    }
                    else
                    {
                        try
                        {
                            string[] days = (dr.Get<string>("DaysName") ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            string result = "";
                            foreach (var day in days)
                            {
                                result += (day.Length >= 3 ? day.Substring(0, 3) : day) + ", ";
                            }
                            registration.Days = result.Length >= 2 ? result.Substring(0, result.Length - 2) : result;
                        }
                        catch (Exception ex)
                        {
                            registration.Days = "**Not Valid**" + dr.Get<string>("DaysName").ToString();
                        }
                    }
                    try
                    {
                        string date = dr.Get<DateTime>("RegistrationDate").ToString();
                        registration.RegistrationDate = date.Substring(0, date.Length - 11);
                    }
                    catch (Exception ex)
                    {
                        registration.RegistrationDate = "Not Valid";
                    }
                    registration.FeasibleTime = dr.Get<string>("FeasibleTime");
                    registration.Scheduled = dr.Get<int>("IsScheduled");
                    list.Add(registration);
                }
                if (data.Counts.Count > 0)
                {
                    studentList.TotalRecords = data.Counts[0].Get<int>("TotalRecords");
                }
                studentList.StudentList = list;
                return studentList;
            }
            catch (Exception ex)
            {
                return studentList;
            }
        }

        public StudentListDO GetUnscheduledStudents()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            var data = new AdminDA().GetUnscheduledStudents();
            foreach (IDictionary<string, object> dr in data.Rows)
            {
                RegistrationDO registration = new RegistrationDO();
                registration.StudentID = dr.Get<string>("StudentID");
                registration.StudentName = dr.Get<string>("Name");
                registration.FatherName = dr.Get<string>("FatherName");
                registration.PhoneNumber = dr.Get<string>("PhoneNumber");
                registration.Email = dr.Get<string>("Email");
                registration.SkypeID = dr.Get<string>("SkypeID");
                registration.DateOfBirth = dr.Get<string>("DateOfBirth");
                registration.City = dr.Get<string>("City");
                registration.Gender = dr.Get<string>("Gender");
                registration.Country = dr.Get<string>("Country");
                registration.Classes = dr.Get<int>("Classes");
                if (dr.Get<int>("Classes") == 7)
                {
                    registration.Days = "All";
                }
                else
                {
                    string[] days = (dr.Get<string>("DaysName") ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string result = "";
                    foreach (var day in days)
                    {
                        result += (day.Length >= 3 ? day.Substring(0, 3) : day) + ", ";
                    }
                    registration.Days = result.Length >= 2 ? result.Substring(0, result.Length - 2) : result;
                }
                registration.FeasibleTime = dr.Get<string>("FeasibleTime");
                string date = dr.Get<DateTime>("RegistrationDate").ToString();
                registration.RegistrationDate = date.Substring(0, date.Length - 11);
                registration.Scheduled = dr.Get<int>("IsScheduled");
                list.Add(registration);
            }
            if (data.Counts.Count > 0)
            {
                studentList.TotalRecords = data.Counts[0].Get<int>("TotalRecords");
            }
            studentList.StudentList = list;
            return studentList;
        }

        public StudentListDO GetScheduledStudents()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            var data = new AdminDA().GetScheduledStudents();
            foreach (IDictionary<string, object> dr in data.Rows)
            {
                RegistrationDO registration = new RegistrationDO();
                registration.StudentID = dr.Get<string>("StudentID");
                registration.StudentName = dr.Get<string>("Name");
                registration.PhoneNumber = dr.Get<string>("PhoneNumber");
                registration.SkypeID = dr.Get<string>("SkypeID");
                registration.Gender = dr.Get<string>("Gender");
                registration.Country = dr.Get<string>("Country");
                registration.Classes = dr.Get<int>("Classes");
                if (dr.Get<int>("Classes") == 7)
                {
                    registration.Days = "All";
                }
                else
                {
                    registration.Days = dr.Get<string>("DaysName");
                }
                registration.ClassTime = dr.Get<string>("ClassTime");
                registration.TutorName = dr.Get<string>("TutorName");
                registration.Description = dr.Get<string>("Description");
                list.Add(registration);
            }
            if (data.Counts.Count > 0)
            {
                studentList.TotalRecords = data.Counts[0].Get<int>("TotalRecords");
            }
            studentList.StudentList = list;
            return studentList;
        }

        public StudentListDO GetTodaySchedule()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            var data = new AdminDA().GetTodaySchedule();
            foreach (IDictionary<string, object> dr in data.Rows)
            {
                RegistrationDO registration = new RegistrationDO();
                registration.StudentID = dr.Get<string>("StudentID");
                registration.StudentName = dr.Get<string>("Name");
                registration.PhoneNumber = dr.Get<string>("PhoneNumber");
                registration.SkypeID = dr.Get<string>("SkypeID");
                registration.Gender = dr.Get<string>("Gender");
                registration.Country = dr.Get<string>("Country");
                registration.Classes = dr.Get<int>("Classes");
                if (dr.Get<int>("Classes") == 7)
                {
                    registration.Days = "All";
                }
                else
                {
                    string[] days = (dr.Get<string>("DaysName") ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string result = "";
                    foreach (var day in days)
                    {
                        result += (day.Length >= 3 ? day.Substring(0, 3) : day) + ", ";
                    }
                    registration.Days = result.Length >= 2 ? result.Substring(0, result.Length - 2) : result;
                }
                registration.ClassTime = dr.Get<string>("ClassTime");
                registration.TutorName = dr.Get<string>("TutorName");
                registration.Description = dr.Get<string>("Description");
                list.Add(registration);
            }
            if (data.Counts.Count > 0)
            {
                studentList.TotalRecords = data.Counts[0].Get<int>("TotalRecords");
            }
            studentList.StudentList = list;
            return studentList;
        }

        public int SaveSchedule(ScheduleDO schedule)
        {
            IDictionary<string, object> r = new AdminDA().SaveSchedule(schedule);
            return r == null ? 0 : r.Get<int>("ScheduleID");
        }

        public string ChangeSchedule(ScheduleDO schedule)
        {
            IDictionary<string, object> r = new AdminDA().ChangeSchedule(schedule);
            return r == null ? "" : r.Str("StudentID");
        }

        public List<ContactUsDO> GetAllContactUs()
        {
            List<ContactUsDO> list = new List<ContactUsDO>();
            foreach (IDictionary<string, object> dr in new AdminDA().GetAllContactUs())
            {
                ContactUsDO registration = new ContactUsDO();
                registration.ContactTopic = dr.Get<string>("ContactTopic");
                registration.ContactEmail = dr.Get<string>("ContactEmail");
                registration.ContactMessage = dr.Get<string>("ContactMessage");
                registration.ContactDate = dr.Get<DateTime>("ContactDate").ToString();
                list.Add(registration);
            }
            return list;
        }

        public int DeleteFeedback(int feedbackID)
        {
            return new AdminDA().DeleteFeedback(feedbackID);
        }

        public RegistrationDO StudentPreview(string studentID)
        {
            RegistrationDO registration = new RegistrationDO();
            IDictionary<string, object> dr = new AdminDA().StudentPreview(studentID);
            if (dr != null)
            {
                registration.StudentID = dr.Get<string>("StudentID");
                registration.StudentName = dr.Get<string>("Name");
                registration.FatherName = dr.Get<string>("FatherName");
                registration.PhoneNumber = dr.Get<string>("PhoneNumber");
                registration.Email = dr.Get<string>("Email");
                registration.SkypeID = dr.Get<string>("SkypeID");
                registration.DateOfBirth = dr.Get<string>("DateOfBirth");
                registration.City = dr.Get<string>("City");
                registration.Gender = dr.Get<string>("Gender");
                registration.Country = dr.Get<string>("Country");
                registration.Classes = dr.Get<int>("Classes");
                registration.Days = dr.Get<string>("DaysName");
                registration.FirstLanguage = dr.Get<string>("FirstLanguage");
                registration.UpdatedRecord = dr.Get<DateTime?>("UpdatedRecord").ToString();
                registration.FeasibleTime = dr.Get<string>("FeasibleTime");
                string date = dr.Get<DateTime>("RegistrationDate").ToString();
                registration.RegistrationDate = date.Substring(0, date.Length - 11);
                registration.Scheduled = dr.Get<int>("IsScheduled");

                registration.ScheduledDays = dr.Get<string>("scheduledDays");
                registration.TutorName = dr.Get<string>("TutorName");
                registration.ClassTime = dr.Get<string>("scheduledClassTime");
                registration.Description = dr.Get<string>("Description");
            }
            return registration;
        }

        public string AddBook(BookDO book)
        {
            IDictionary<string, object> r = new AdminDA().AddBook(book);
            return r == null ? string.Empty : r.Str("BookID");
        }

        public int ChangeBook(BookDO book)
        {
            return new AdminDA().ChangeBook(book);
        }

        public List<BookDO> GetAllBooks()
        {
            List<BookDO> list = new List<BookDO>();
            foreach (IDictionary<string, object> dr in new AdminDA().GetAllBooks())
            {
                BookDO book = new BookDO();
                book.BookID = dr.Get<int>("BookID");
                book.BookTilte = dr.Get<string>("BookTilte");
                book.AutherName = dr.Get<string>("AuthorName");
                book.ImagePath = dr.Get<string>("ImagePath");
                book.FilePath = dr.Get<string>("FilePath");
                book.BookType = dr.Get<string>("BookType");
                book.Detail = dr.Get<string>("Detail");
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
            return list;
        }

        public int DeleteBook(int BookID)
        {
            return new AdminDA().DeleteBook(BookID);
        }

        public List<AdminUserDO> GetAllAdmins()
        {
            List<AdminUserDO> list = new List<AdminUserDO>();
            foreach (IDictionary<string, object> dr in new AdminDA().GetAllAdmins())
            {
                AdminUserDO admin = new AdminUserDO();
                admin.Id = dr.Get<int>("Id");
                admin.AdminName = dr.Get<string>("AdminName");
                admin.AdminEmail = dr.Get<string>("AdminEmail");
                admin.AdminPassword = dr.Get<string>("AdminPassword");
                list.Add(admin);
            }
            return list;
        }

        public AdminUserDO GetAdminById(int id)
        {
            AdminUserDO admin = new AdminUserDO();
            IDictionary<string, object> dr = new AdminDA().GetAdminById(id);
            if (dr != null)
            {
                admin.Id = dr.Get<int>("Id");
                admin.AdminName = dr.Get<string>("AdminName");
                admin.AdminEmail = dr.Get<string>("AdminEmail");
                admin.AdminPassword = dr.Get<string>("AdminPassword");
            }
            return admin;
        }

        public bool AdminEmailExists(string email, int excludeId)
        {
            return new AdminDA().AdminEmailExists(email, excludeId) > 0;
        }

        public int SaveAdmin(AdminUserDO admin)
        {
            return new AdminDA().SaveAdmin(admin);
        }

        public int UpdateAdmin(AdminUserDO admin)
        {
            return new AdminDA().UpdateAdmin(admin);
        }

        public int DeleteAdmin(int id)
        {
            return new AdminDA().DeleteAdmin(id);
        }
    }
}
