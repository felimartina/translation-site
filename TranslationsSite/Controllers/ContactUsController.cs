using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web.Mvc;
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
                var toAddress = ConfigurationManager.AppSettings["EmailTo"];
                var fromAddress = email;
                var subject = "You received an enquiry from " + name;
                var sbMessage = new StringBuilder();

                sbMessage.Append("Name: " + name + "\n");
                sbMessage.Append("Email: " + email + "\n");
                sbMessage.Append("Telephone: " + phone + "\n\n");
                sbMessage.Append(message);

                //start email Thread
                var tEmail = new Thread(
                    () => EmailHelper.SendEmail(toAddress, fromAddress, subject, sbMessage.ToString()));
                tEmail.Start();

                return JavaScript("true");
            }
            catch (Exception ex)
            {
                log.Error("There was a problem trying to send an enquiry", ex);
                return JavaScript("false");
            }

        }

    }
}
