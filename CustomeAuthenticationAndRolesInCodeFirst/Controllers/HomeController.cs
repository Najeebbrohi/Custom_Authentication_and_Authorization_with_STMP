using CustomeAuthenticationAndRolesInCodeFirst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CustomeAuthenticationAndRolesInCodeFirst.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(GmailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SendEmail(model);
                    ViewBag.Message = "Your message has been sent successfully.";
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error sending email: {ex.Message}";
            }

            return View();
        }
        private void SendEmail(GmailViewModel model)
        {
            string toEmail = "najeebbrohi9477@gmail.com"; // Replace with your email address

            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress(model.Email);
            mail.Subject = model.Subject;
            mail.Body = $"From: {model.Name} ({model.Email})\n\n{model.Message}";

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("najeebbrohi9477@gmail.com", "lotn kimm znro yzcg");
                smtp.Port = 587;

                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, display an error message, etc.)
                    throw new Exception($"Error sending email: {ex.Message}", ex);
                }
            }
        }

    }
}