using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using System;
using System.Net.Mail;

namespace HCRGUniversityMgtApp.Infrastructure.ApplicationServices
{
    public class EMailService : IEMail
    {
        public string SendEmail(string msg, string sender, string subject)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["FromAddress"]);
                mail.To.Add(sender);
                mail.Subject = subject;
                mail.Body = msg;
                mail.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = System.Configuration.ConfigurationSettings.AppSettings["SMTPServer"];
                smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["MailPort"]);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings["MailUserName"], System.Configuration.ConfigurationSettings.AppSettings["MailPassword"]);
                smtp.Send(mail);
                return Global.GlobalConst.Message.EmailSentSuccessfully;
            }
            catch
            {
                return Global.GlobalConst.Message.ErrorOccouredWhileSendingEmail;
            }

        }

        public string SendRandomPasswordEmail(string msg, string sender, string subject)
        {
            return SendEmail( msg, sender, subject);
        }

    }
}
