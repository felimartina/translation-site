using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using Logging;
using TranslationsSite.Helpers;

namespace TranslationsSite.Controllers
{
    public class ContactUsController : Controller
    {
        private static readonly Logger log = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            return View();
        }

        public JavaScriptResult SendMessage(string name, string phone, string email, string message)
        {
            try
            {
                //prepare email
                var toAddress = ConfigurationManager.AppSettings[Constants.SETTING_EMAIL_TO];
                var fromAddress = email;
                var subject = string.Format("You received an enquiry from {0}", name);
                string htmlMessage;
                using (var reader = new StreamReader(ControllerContext.HttpContext.Server.MapPath("~/Templates/EnquiryReceived.html")))
                {
                    htmlMessage = reader.ReadToEnd();
                }
                htmlMessage = htmlMessage.Replace("{{NAME}}", name)
                                         .Replace("{{PHONE}}", phone)
                                         .Replace("{{EMAIL}}", email)
                                         .Replace("{{MESSAGE}}", message);
                //start email Thread
                var tEmail = new Thread(
                    () => EmailHelper.SendEmail(toAddress, fromAddress, subject, htmlMessage));
                tEmail.Start();
                //Also send a confirmation email to the user
                EmailHelper.SendConfirmationEmail(name, email);

                return JavaScript("true");
            }
            catch (Exception ex)
            {
                log.Error("There was a problem trying to send an enquiry", ex);
                log.Error("Enquiry Information: Name: {0}, Email: {1}, Phone: {2}, Message {3}",
                    name, email, phone, message);
                return JavaScript("false");
            }

        }
    }
}
