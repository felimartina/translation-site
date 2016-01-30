using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Logging;

namespace TranslationsSite.Helpers
{
    public class EmailHelper
    {
        private static readonly Logger log = new Logger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void SendEmail(string toAddress, string fromAddress, string subject, string message, bool html = true)
        {
            using (var smtpClient = 
                new SmtpClient(
                    ConfigurationManager.AppSettings["SmtpHost"], 
                    Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"])))
            {
                smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpEnableSsl"]);
                smtpClient.UseDefaultCredentials = false;
                var loginInfo = 
                    new NetworkCredential(
                        ConfigurationManager.AppSettings["SmtpUserName"], 
                        ConfigurationManager.AppSettings["SmtpUserPass"]);
                smtpClient.Credentials = loginInfo;

                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromAddress);
                    mail.To.Add(new MailAddress(toAddress));
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = html;

                    try
                    {
                        smtpClient.Send(mail);
                        log.Info("Email successfully sent.");
                    }
                    catch (SmtpFailedRecipientsException ex)
                    {
                        foreach (SmtpFailedRecipientException t in ex.InnerExceptions)
                        {
                            var status = t.StatusCode;
                            if (status == SmtpStatusCode.MailboxBusy ||
                                status == SmtpStatusCode.MailboxUnavailable)
                            {
                                log.Error("Email Delivery failed - retrying in 5 seconds.");
                                System.Threading.Thread.Sleep(5000);
                                //resend
                                smtpClient.Send(mail);
                            }
                            else
                            {
                                log.Error("Failed to deliver message to {0}", t.FailedRecipient);
                            }
                        }
                    }
                    catch (SmtpException Se)
                    {
                        // handle exception here
                        log.Error(Se.ToString());
                    }

                    catch (Exception ex)
                    {
                        log.Error(ex.ToString());
                    }
                }
            }
        }
    }
}