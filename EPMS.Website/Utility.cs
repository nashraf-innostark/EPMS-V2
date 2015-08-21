using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace EPMS.Website
{
    public class Utility
    {
        public static void SendEmailAsync(string email, string subject, string body)
        {

            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            string fromPwd = ConfigurationManager.AppSettings["FromPassword"];
            string fromDisplayName = ConfigurationManager.AppSettings["FromDisplayNameA"];
            //string cc = ConfigurationManager.AppSettings["CC"];
            //string bcc = ConfigurationManager.AppSettings["BCC"];

            //Getting the file from config, to send
            MailMessage oEmail = new MailMessage
            {
                From = new MailAddress(fromAddress, fromDisplayName),
                Subject = subject,
                IsBodyHtml = true,
                Body = body,
                Priority = MailPriority.High
            };
            oEmail.To.Add(email);
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            string enableSsl = ConfigurationManager.AppSettings["EnableSSL"];
            SmtpClient client = new SmtpClient(smtpServer, Convert.ToInt32(smtpPort))
            {
                EnableSsl = enableSsl == "1",
                Credentials = new NetworkCredential(fromAddress, fromPwd)
            };

            client.Send(oEmail);

        }
    }
}