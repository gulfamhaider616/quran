using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearnFreeQuran.Library;
using LearnFreeQuran.Business;
using System.Security.Policy;

namespace LearnFreeQuran.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Registration()
        {
            return View();
        }
        public ActionResult SaveUser(string name, string email, string password)
        {
            UserDO user = new UserDO();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            bool result = new UserBA().SaveUser(user);
            if (result)
            {
                return View("UserLogin");
            }
            else
            {
                return View("Registration");
            }
        }
        public ActionResult UserLogin()
        {
            return View();
        }
        public ActionResult VerifyUser(string email, string password)
        {
            var user = new UserBA().VerifyUser(email, password);
            if (user != null)
            {
                Session["name"] = user.Name;
                Session["Email"] = user.Email;
                Session["Bookmarkid"] = "#"+user.BookmarkId;
                Session["Chapterid"] = user.ChapterID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("UserLogin");
            }
        }
        public ActionResult GetUserProfile(int userID)
        {
            return View(new UserBA().GetUserProfile(userID));
        }
        public JsonResult Addookmark(string id)
        {
            string url = Request.Url.AbsoluteUri; ;
            if (Session["Email"] != null)
            {
                UserDO user = new UserDO();
                user.BookmarkId = id;
                user.Email = Session["Email"].ToString();
                bool result = new UserBA().Addookmark(user);  
                if (result)
                {
                    Session["Bookmarkid"] = "#" + id;
                    Session["Chapterid"] = id.Split('-')[0];
                }
                url = url.Split('U')[0] + "quran_reading?ChapterID="+ id.Split('-')[0]+ "#" + id;
                return Json(url, JsonRequestBehavior.AllowGet);
            }
            else
            {
                url = url.Split('U')[0] + "User/UserLogin";
                return Json(url, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            Session["name"] = null;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        //public void sendMessage()
        //{
        //    // Find your Account Sid and Token at twilio.com/console
        //    // DANGER! This is insecure. See http://twil.io/secure
        //    const string accountSid = "AC1053695f87d9c6bede630a6ca9fd0c38";
        //    const string authToken = "7fa24e64cb192175f904da9d774f729e";
        //    TwilioClient.Init(accountSid, authToken);
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        //                                        | SecurityProtocolType.Tls11
        //                                        | SecurityProtocolType.Tls12
        //                                        | SecurityProtocolType.Ssl3;
        //    var message = MessageResource.Create(
        //        body: "Hello Hasnain, Why are you making parking App? When will it get complete?",
        //        from: new Twilio.Types.PhoneNumber("+12342001840"),
        //        to: new Twilio.Types.PhoneNumber("+923404428351")
        //    );
        //    Console.WriteLine(message.Sid);
        //}
        //public string SendEmail(EmailDO model)
        //{

        //    DateTime t1 = DateTime.Now;
        //    DateTime t2 = Convert.ToDateTime("11:00:00 AM");
        //    int i = DateTime.Compare(t1, t2);
        //    int count = 0;
        //    UserBA ba = new UserBA();
        //    var userList = ba.GetAllUsers();
        //    string mailTo = "ahmedshair198@gmail.com";
        //    string bcc = "ahmedshair198@gmail.com";
        //    foreach (var user in userList)
        //    {
        //        bcc += "," + user.Email;
        //    }
        //    int port = 587;
        //    string host = "smtp.gmail.com";
        //    string username = "ecareerspk@gmail.com";
        //    string password = "32129241770ab";
        //    string mailFrom = "ecareerspk@gmail.com";
        //    string mailTitle = "Thanks for Registration | Mcqsprep.com";
        //    string mailMessage = @"Dear User,<br/><br/>

        //        Thank you so much for your registration to <a href='http://mcqsprep.com' target='_blank'> Mcqsprep.com</a>.<br/><br/>

        //        <a href='http://mcqsprep.com' target='_blank'>Mcqsprep</a> is 100 % free platform and we'll not charge anything from the users. It's just for the help of those people who want to prepare themselves and looking for a job in Pakistan. Our team is continuously working to add more data in our web platform. We are also requesting you to help us to improve this system. 
        //        <br/><br/>
        //        We are also going to build a new module in which we allow the users to add their own mcqs according to their subject and earn from our website on the daily basis. This will increase your daily earning. 
        //         <br/><br/>
        //        Please give us feedback to improve this, we will appreciate you for this act. 
        //        <br/><br/>
        //        If you have any question, feel free to contact us.
        //        <br/><br/>
        //        Thanks,<br/>

        //        Ahmed Shair<br/><br/>

        //        CTO <a href='http://mcqsprep.com' target='_blank'>Mcqsprep.com</a>";

        //    using (SmtpClient client = new SmtpClient())
        //    {
        //        MailAddress from = new MailAddress(mailFrom);
        //        MailMessage message = new MailMessage
        //        {
        //            From = from
        //        };
        //        message.To.Add(mailTo);
        //        message.Bcc.Add(bcc);
        //        message.Subject = mailTitle;
        //        message.Body = mailMessage;
        //        message.IsBodyHtml = true;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;
        //        client.Host = host;
        //        client.Port = port;
        //        client.EnableSsl = true;
        //        client.Credentials = new NetworkCredential
        //        {
        //            UserName = username,
        //            Password = password
        //        };
        //        client.Send(message);
        //    }
        //    return "";
        //}
    }
}