using LearnFreeQuran.DataAccess;
using LearnFreeQuran.Library;
using System.Data;

namespace LearnFreeQuran.Business
{
    public class AdminBA
    {
        public async Task<string> VerifyAdminAsync(string adminemail, string adminpassword)
        {
            DataSet dataset = await new AdminDA().VerifyAdminAsync(adminemail, adminpassword);
            string adminName = "";

            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    adminName = r["AdminName"].ToString();
                }
            }

            return adminName;
        }

        public async Task<StudentListDO> GetAllStudentsAsync()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            DataSet dataset = await new AdminDA().GetAllStudentsAsync();

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
                                string[] days = dr.Field<string>("DaysName").Split(',');
                                string result = "";
                                foreach (var day in days)
                                {
                                    result += day.Substring(0, 3) + ", ";
                                }
                                registration.Days = result.Substring(0, result.Length - 2);
                            }
                            catch
                            {
                                registration.Days = "**Not Valid**" + dr.Field<string>("DaysName").ToString();
                            }
                        }

                        try
                        {
                            string date = dr.Field<DateTime>("RegistrationDate").ToString();
                            registration.RegistrationDate = date.Substring(0, date.Length - 11);
                        }
                        catch
                        {
                            registration.RegistrationDate = "Not Valid";
                        }

                        registration.FeasibleTime = dr.Field<string>("FeasibleTime");
                        registration.Scheduled = dr.Field<int>("IsScheduled");
                        list.Add(registration);
                    }

                    if (dataset.Tables.Count > 1 && dataset.Tables[1].Rows.Count > 0)
                    {
                        DataRow r = dataset.Tables[1].Rows[0];
                        studentList.TotalRecords = Convert.ToInt32(r["TotalRecords"]);
                    }
                }

                studentList.StudentList = list;
                return studentList;
            }
            catch
            {
                return studentList;
            }
        }

        public async Task<StudentListDO> GetUnscheduledStudentsAsync()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            DataSet dataset = await new AdminDA().GetUnscheduledStudentsAsync();

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
                        string[] days = dr.Field<string>("DaysName").Split(',');
                        string result = "";
                        foreach (var day in days)
                        {
                            result += day.Substring(0, 3) + ", ";
                        }
                        registration.Days = result.Substring(0, result.Length - 2);
                    }

                    registration.FeasibleTime = dr.Field<string>("FeasibleTime");
                    string date = dr.Field<DateTime>("RegistrationDate").ToString();
                    registration.RegistrationDate = date.Substring(0, date.Length - 11);
                    registration.Scheduled = dr.Field<int>("IsScheduled");
                    list.Add(registration);
                }

                if (dataset.Tables.Count > 1 && dataset.Tables[1].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[1].Rows[0];
                    studentList.TotalRecords = Convert.ToInt32(r["TotalRecords"]);
                }
            }

            studentList.StudentList = list;
            return studentList;
        }

        public async Task<StudentListDO> GetScheduledStudentsAsync()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            DataSet dataset = await new AdminDA().GetScheduledStudentsAsync();

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

                if (dataset.Tables.Count > 1 && dataset.Tables[1].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[1].Rows[0];
                    studentList.TotalRecords = Convert.ToInt32(r["TotalRecords"]);
                }
            }

            studentList.StudentList = list;
            return studentList;
        }

        public async Task<StudentListDO> GetTodayScheduleAsync()
        {
            StudentListDO studentList = new StudentListDO();
            List<RegistrationDO> list = new List<RegistrationDO>();
            DataSet dataset = await new AdminDA().GetTodayScheduleAsync();

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
                        string[] days = dr.Field<string>("DaysName").Split(',');
                        string result = "";
                        foreach (var day in days)
                        {
                            result += day.Substring(0, 3) + ", ";
                        }
                        registration.Days = result.Substring(0, result.Length - 2);
                    }

                    registration.ClassTime = dr.Field<string>("ClassTime");
                    registration.TutorName = dr.Field<string>("TutorName");
                    registration.Description = dr.Field<string>("Description");
                    list.Add(registration);
                }

                if (dataset.Tables.Count > 1 && dataset.Tables[1].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[1].Rows[0];
                    studentList.TotalRecords = Convert.ToInt32(r["TotalRecords"]);
                }
            }

            studentList.StudentList = list;
            return studentList;
        }

        public async Task<int> SaveScheduleAsync(ScheduleDO schedule)
        {
            DataSet dataset = await new AdminDA().SaveScheduleAsync(schedule);
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

        public async Task<string> ChangeScheduleAsync(ScheduleDO schedule)
        {
            DataSet dataset = await new AdminDA().ChangeScheduleAsync(schedule);
            string studentID = "";

            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    studentID = r["StudentID"].ToString();
                }
            }

            return studentID;
        }

        public async Task<List<ContactUsDO>> GetAllContactUsAsync()
        {
            List<ContactUsDO> list = new List<ContactUsDO>();
            DataSet dataset = await new AdminDA().GetAllContactUsAsync();

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

        public async Task<int> DeleteFeedbackAsync(int feedbackID)
        {
            return await new AdminDA().DeleteFeedbackAsync(feedbackID);
        }

        public async Task<RegistrationDO> StudentPreviewAsync(string studentID)
        {
            DataSet dataset = await new AdminDA().StudentPreviewAsync(studentID);
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

                    registration.ScheduledDays = dr.Field<string>("scheduledDays");
                    registration.TutorName = dr.Field<string>("TutorName");
                    registration.ClassTime = dr.Field<string>("scheduledClassTime");
                    registration.Description = dr.Field<string>("Description");
                }
            }

            return registration;
        }

        public async Task<string> AddBookAsync(BookDO book)
        {
            DataSet dataset = await new AdminDA().AddBookAsync(book);
            string bookID = string.Empty;

            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    bookID = r["BookID"].ToString();
                }
            }

            return bookID;
        }

        public async Task<int> ChangeBookAsync(BookDO book)
        {
            return await new AdminDA().ChangeBookAsync(book);
        }

        public async Task<List<BookDO>> GetAllBooksAsync()
        {
            List<BookDO> list = new List<BookDO>();
            DataSet dataset = await new AdminDA().GetAllBooksAsync();

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

        public async Task<int> DeleteBookAsync(int bookID)
        {
            return await new AdminDA().DeleteBookAsync(bookID);
        }
    }
}