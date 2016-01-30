using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Logging;
using TranslationsSite.Helpers;

namespace TranslationsSite.Controllers
{
    public class GetQuoteController : Controller
    {
        private static readonly Logger log = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            return View();
        }

        public JavaScriptResult QuoteRequest(string name, string phone, string email, 
            string projectType, string languages, HttpPostedFileBase file, 
            string wordCount, string deadline, string details)
        {
            try
            {
                log.Debug("testing");
                //prepare email
                var toAddress = ConfigurationManager.AppSettings[Constants.SETTING_EMAIL_TO];
                var fromAddress = email;
                var subject = string.Format("You received an enquiry from {0}", name);
                string htmlMessage;
                using (var reader = new StreamReader(ControllerContext.HttpContext.Server.MapPath("~/Templates/QuoteRequestReceived.html")))
                {
                    htmlMessage = reader.ReadToEnd();
                }
                htmlMessage = htmlMessage.Replace("{{NAME}}", name)
                                         .Replace("{{PHONE}}", phone)
                                         .Replace("{{EMAIL}}", email)
                                         .Replace("{{PROJECT_TYPE}}", projectType)
                                         .Replace("{{LANGUAGES}}", languages)
                                         .Replace("{{WORD_COUNT}}", wordCount)
                                         .Replace("{{DEADLINE}}", deadline)
                                         .Replace("{{DETAILS}}", details);
                //Attachment
                List<Attachment> attachments = null;
                if (file != null)
                {
                    attachments = new List<Attachment> { new Attachment(file.InputStream, file.FileName) };
                }
                    
                //start email Thread
                var tEmail = new Thread(
                    () => EmailHelper.SendEmail(toAddress, fromAddress, subject, htmlMessage, attachments));
                tEmail.Start();
                //Also send a confirmation email to the user
                EmailHelper.SendConfirmationEmail(name, email);

                return JavaScript("true");
            }
            catch (Exception ex)
            {
                log.Error("There was a problem trying to send an enquiry", ex);
                log.Error("Enquiry Information: Name: {0}, Email: {1}, Phone: {2}, Message {3}",
                    name, email, phone, details);
                return JavaScript("false");
            }

        }

    }
}
