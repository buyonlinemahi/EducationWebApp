using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using System;
using System.Net.Mail;

namespace HCRGUniversityApp.Infrastructure.ApplicationServices
{
  public  class EMailService:IEMail
    {
      public void SendEmail(string subject, string  msg,string sender)
      {         
              MailMessage mail = new MailMessage();
              mail.From = new MailAddress(System.Configuration.ConfigurationSettings.AppSettings["FromAddress"]);
              mail.To.Add(sender);
              mail.Subject = subject;
              mail.Body = msg;
              mail.IsBodyHtml = true;
              System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
              smtp.Host = System.Configuration.ConfigurationSettings.AppSettings["SMTPServer"];
              smtp.Port =Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["MailPort"]);
              smtp.EnableSsl = true;              
              smtp.UseDefaultCredentials = false;
              smtp.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings["MailUserName"], System.Configuration.ConfigurationSettings.AppSettings["MailPassword"]);
              smtp.Send(mail);
   
      }
    }
}
